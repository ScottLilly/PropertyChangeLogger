namespace PropertyChangeLogger
{
    public class PropertyChange
    {
        public string PropertyName { get; }
        public object OriginalValue { get; }
        public object CurrentValue { get; }

        public PropertyChange(string propertyName, object originalValue, object currentValue)
        {
            PropertyName = propertyName;
            OriginalValue = originalValue;
            CurrentValue = currentValue;
        }
    }
}