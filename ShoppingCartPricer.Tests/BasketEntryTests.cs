using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingCartPricer.Tests
{
    [TestClass]
    public class BasketEntryTests
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [ExpectedException(typeof(System.ArgumentException))]
        public void BasketEntry_DoesntAllow0Quantity(int quantity)
        {
            _ = new BasketEntry(new Item(name: "Test", price: 1), quantity);
        }
    }
}
