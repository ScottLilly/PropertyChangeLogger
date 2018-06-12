using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PropertyChangeLogger
{
    public abstract class PropertyChangeLoggingClass
    {
        private readonly SortedList<string, object> _initialPropertyValues = 
            new SortedList<string, object>();

        protected PropertyChangeLoggingClass()
        {
            SetInitialPropertyValues();
        }

        protected void SetInitialPropertyValues()
        {
            IEnumerable<PropertyInfo> propertiesToLog = PropertiesToLog;

            foreach(PropertyInfo property in propertiesToLog)
            {
                if(_initialPropertyValues.ContainsKey(property.Name))
                {
                    _initialPropertyValues[property.Name] = GetType().GetProperty(property.Name)?.GetValue(this, null);
                }
                else
                {
                    _initialPropertyValues.Add(property.Name, GetType().GetProperty(property.Name)?.GetValue(this, null));
                }
            }
        }

        [DoNotLogChangesToThisProperty]
        public bool HasChangedProperties => (ChangedProperties.Count > 0);

        [DoNotLogChangesToThisProperty]
        public List<PropertyChange> ChangedProperties
        {
            get
            {
                List<PropertyChange> changedProperties = new List<PropertyChange>();

                IEnumerable<PropertyInfo> propertiesThatNeedLogging = PropertiesToLog;

                foreach(PropertyInfo property in propertiesThatNeedLogging.Where(x => _initialPropertyValues.ContainsKey(x.Name)))
                {
                    // Ignore properties that are a list, since those can be changed by adding, editing, or deleting values in the list.
                    // For now, those are more complex than I want to deal with.
                    if(typeof(IList).IsAssignableFrom(property.PropertyType))
                    {
                        continue;
                    }

                    // If the property is a type that has a base of PropertyChangeLoggingClass, check it for changes.
                    if(typeof(PropertyChangeLoggingClass).IsAssignableFrom(property.PropertyType))
                    {
                        foreach(PropertyChange propertyChange in ((PropertyChangeLoggingClass)property.GetValue(this, null)).ChangedProperties)
                        {
                            changedProperties.Add(
                                new PropertyChange(
                                    property.Name + "." + propertyChange.PropertyName, 
                                    propertyChange.OriginalValue, 
                                    propertyChange.CurrentValue));
                        }

                        continue;
                    }

                    // See if the property changed from its original value.
                    var originalValue = _initialPropertyValues[property.Name];
                    var currentValue = property.GetValue(this, null);

                    if(((originalValue == null) && (currentValue != null)) ||
                       ((originalValue != null) && (currentValue == null)))
                    {
                        changedProperties.Add(
                            property.HasCustomAttributeOfType(typeof(DoNotLogValuesWhenThisPropertyChangesAttribute)) ?
                            new PropertyChange(property.Name, null, null) :
                            new PropertyChange(property.Name, originalValue, currentValue));
                    }
                    else if(originalValue == null)
                    {
                    }
                    else if(!currentValue.Equals(originalValue))
                    {
                        changedProperties.Add(
                            property.HasCustomAttributeOfType(typeof(DoNotLogValuesWhenThisPropertyChangesAttribute)) ?
                            new PropertyChange(property.Name, null, null) :
                            new PropertyChange(property.Name, originalValue, currentValue));
                    }
                }

                return changedProperties;
            }
        }

        [DoNotLogChangesToThisProperty]
        private IEnumerable<PropertyInfo> PropertiesToLog
        {
            get
            {
                return GetType().GetProperties()
                    .Where(x => x.GetCustomAttributes(typeof(DoNotLogChangesToThisPropertyAttribute), true).Length == 0);
            }
        }
    }
}
