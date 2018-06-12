using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestPropertyChangeLogger
{
    [TestClass]
    public class TestPropertyChangeLogging
    {
        private const string CUSTOMER_ORIGINAL_NAME = "CUSTNAME1";
        private const string CUSTOMER_ORIGINAL_PASSWORD = "PASS1";
        private const double CUSTOMER_ORIGINAL_COMPUTED_VALUE = 1.11;

        private const string CUSTOMER_MODIFIED_NAME = "CUSTNAME2";
        private const string CUSTOMER_MODIFIED_PASSWORD = "PASS2";
        private const double CUSTOMER_MODIFIED_COMPUTED_VALUE = 2.22;

        [TestMethod]
        public void EmptyConstructor_NoPropertiesChanged()
        {
            Customer customer = new Customer();

            Assert.IsFalse(customer.HasChangedProperties);
            Assert.AreEqual(0, customer.ChangedProperties.Count);
        }

        [TestMethod]
        public void EmptyConstructor_OneLoggedPropertyChanged()
        {
            Customer customer = new Customer();

            customer.Name = CUSTOMER_ORIGINAL_NAME;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(1, customer.ChangedProperties.Count);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Name").OriginalValue);
            Assert.AreEqual(CUSTOMER_ORIGINAL_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").CurrentValue);
        }

        [TestMethod]
        public void EmptyConstructor_OneLoggedWithoutValuesPropertyChanged()
        {
            Customer customer = new Customer();

            customer.Password = CUSTOMER_ORIGINAL_PASSWORD;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").CurrentValue);
        }

        [TestMethod]
        public void EmptyConstructor_OneDoNotLogPropertyChanged()
        {
            Customer customer = new Customer();

            customer.ComputedValue = CUSTOMER_ORIGINAL_COMPUTED_VALUE;

            Assert.IsFalse(customer.HasChangedProperties);
        }

        [TestMethod]
        public void EmptyConstructor_OneLoggedOneLoggedWithoutValuesPropertyChanged()
        {
            Customer customer = new Customer();

            customer.Name = CUSTOMER_ORIGINAL_NAME;
            customer.Password = CUSTOMER_ORIGINAL_PASSWORD;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(2, customer.ChangedProperties.Count);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Name").OriginalValue);
            Assert.AreEqual(CUSTOMER_ORIGINAL_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").CurrentValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").CurrentValue);
        }

        [TestMethod]
        public void PopulatedConstructor_NoPropertiesChanged()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            Assert.IsFalse(customer.HasChangedProperties);
            Assert.AreEqual(0, customer.ChangedProperties.Count);
        }

        [TestMethod]
        public void PopulatedConstructor_OneLoggedPropertyChanged()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.Name = CUSTOMER_MODIFIED_NAME;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(1, customer.ChangedProperties.Count);
            Assert.AreEqual(CUSTOMER_ORIGINAL_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").OriginalValue);
            Assert.AreEqual(CUSTOMER_MODIFIED_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").CurrentValue);
        }

        [TestMethod]
        public void PopulatedConstructor_OneLoggedWithoutValuesPropertyChanged()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.Password = CUSTOMER_MODIFIED_PASSWORD;

            Assert.AreEqual(1, customer.ChangedProperties.Count);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").CurrentValue);
        }

        [TestMethod]
        public void PopulatedConstructor_OneDoNotLogPropertyChanged()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.ComputedValue = CUSTOMER_MODIFIED_COMPUTED_VALUE;

            Assert.IsFalse(customer.HasChangedProperties);
        }

        [TestMethod]
        public void PopulatedConstructor_OneLoggedOneLoggedWithoutValuesPropertyChanged()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.Name = CUSTOMER_MODIFIED_NAME;
            customer.Password = CUSTOMER_MODIFIED_PASSWORD;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(2, customer.ChangedProperties.Count);
            Assert.AreEqual(CUSTOMER_ORIGINAL_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").OriginalValue);
            Assert.AreEqual(CUSTOMER_MODIFIED_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").CurrentValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").CurrentValue);
        }

        [TestMethod]
        public void PopulatedConstructor_OneLoggedPropertyChangedToNull()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.Name = null;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(1, customer.ChangedProperties.Count);
            Assert.AreEqual(CUSTOMER_ORIGINAL_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Name").CurrentValue);
        }

        [TestMethod]
        public void PopulatedConstructor_OneLoggedWithoutValuesPropertyChangedToNull()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.Password = null;

            Assert.AreEqual(1, customer.ChangedProperties.Count);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").CurrentValue);
        }

        [TestMethod]
        public void PopulatedConstructor_OneLoggedOneLoggedWithoutValuesPropertyChangedToNull()
        {
            Customer customer = new Customer(CUSTOMER_ORIGINAL_NAME, CUSTOMER_ORIGINAL_PASSWORD, CUSTOMER_ORIGINAL_COMPUTED_VALUE);

            customer.Name = null;
            customer.Password = null;

            Assert.IsTrue(customer.HasChangedProperties);
            Assert.AreEqual(2, customer.ChangedProperties.Count);
            Assert.AreEqual(CUSTOMER_ORIGINAL_NAME, customer.ChangedProperties.First(x => x.PropertyName == "Name").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Name").CurrentValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").OriginalValue);
            Assert.AreEqual(null, customer.ChangedProperties.First(x => x.PropertyName == "Password").CurrentValue);
        }
    }
}