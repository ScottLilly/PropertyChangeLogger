using PropertyChangeLogger;

namespace TestPropertyChangeLogger
{
    public class Customer : PropertyChangeLoggingClass
    {
        public string Name { get; set; }
        [DoNotLogValuesWhenThisPropertyChanges]
        public string Password { get; set; }
        [DoNotLogChangesToThisProperty]
        public double ComputedValue { get; set; }

        public Customer()
        {
        }

        public Customer(string name, string password, double computedValue)
        {
            Name = name;
            Password = password;
            ComputedValue = computedValue;

            SetInitialPropertyValues();
        }
    }
}