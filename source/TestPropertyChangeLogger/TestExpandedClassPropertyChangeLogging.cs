using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestPropertyChangeLogger
{
    [TestClass]
    public class TestExpandedClassPropertyChangeLogging
    {
        private const string CUSTOMER_ORIGINAL_NAME = "CUSTNAME1";
        private const string CUSTOMER_ORIGINAL_PASSWORD = "PASS1";
        private const double CUSTOMER_ORIGINAL_COMPUTED_VALUE = 1.11;

        private const string CUSTOMER_MODIFIED_NAME = "CUSTNAME2";
        private const string CUSTOMER_MODIFIED_PASSWORD = "PASS2";
        private const double CUSTOMER_MODIFIED_COMPUTED_VALUE = 2.22;

        [TestMethod]
        public void PopulatedConstructor_NoPropertiesChanged()
        {
            Address address = new Address("1234 Main Street", "Houston", "Texas", new PostalCode("77777", "6666"));
            ExpandedCustomer customer = new ExpandedCustomer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE, address);

            Assert.IsFalse(customer.HasChangedProperties);
            Assert.AreEqual(0, customer.ChangedProperties.Count);
        }

        [TestMethod]
        public void PopulatedConstructor_OneChildPropertyChanged()
        {
            Address address = new Address("1234 Main Street", "Houston", "Texas", new PostalCode("77777", "6666"));
            ExpandedCustomer customer = new ExpandedCustomer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE, address);

            customer.BillingAddress.PostCode.Prefix = "11111";

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(1, customer.ChangedProperties.Count);
            Assert.AreEqual("BillingAddress.PostCode.Value", customer.ChangedProperties[0].PropertyName);
            Assert.AreEqual("77777-6666", customer.ChangedProperties[0].OriginalValue);
            Assert.AreEqual("11111-6666", customer.ChangedProperties[0].CurrentValue);
        }
    }
}
