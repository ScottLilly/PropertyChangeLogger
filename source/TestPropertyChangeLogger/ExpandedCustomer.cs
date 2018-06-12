using System.Collections.Generic;

using PropertyChangeLogger;

namespace TestPropertyChangeLogger
{
    public class ExpandedCustomer : PropertyChangeLoggingClass
    {
        public string Name { get; set; }
        [DoNotLogValuesWhenThisPropertyChanges]
        public string Password { get; set; }
        [DoNotLogChangesToThisProperty]
        public double ComputedValue { get; set; }
        public Address BillingAddress { get; set; }
        public List<string> EmployeeName { get; set; } // List properties are currently not checked for changes

        public ExpandedCustomer(string name, string password, double computedValue, Address billingAddress)
        {
            Name = name;
            Password = password;
            ComputedValue = computedValue;
            BillingAddress = billingAddress;

            SetInitialPropertyValues();
        }
    }
}