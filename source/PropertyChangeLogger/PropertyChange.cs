namespace PropertyChangeLogger
{
    public class PropertyChange
    {
        public string PropertyName { get; set; }
        public object OriginalValue { get; set; }
        public object CurrentValue { get; set; }

        public PropertyChange(string propertyName, object originalValue, object currentValue)
        {
            PropertyName = propertyName;
            OriginalValue = originalValue;
            CurrentValue = currentValue;
        }
    }
}
