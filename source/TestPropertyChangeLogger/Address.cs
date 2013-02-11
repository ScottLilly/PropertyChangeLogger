using PropertyChangeLogger;

namespace TestPropertyChangeLogger
{
    public class Address : PropertyChangeLoggingClass
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public PostalCode PostCode { get; set; }

        public Address()
        {
        }

        public Address(string streetAddress, string city, string state, PostalCode postCode)
        {
            StreetAddress = streetAddress;
            City = city;
            State = state;
            PostCode = postCode;

            SetInitialPropertyValues();
        }
    }
}
