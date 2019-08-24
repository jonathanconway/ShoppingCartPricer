using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShoppingCartPricer.Tests
{
    [TestClass]
    public class ItemTests
    {
        [DataTestMethod]
        [DataRow("Test", 0)]
        [DataRow("Test", -1)]
        [DataRow("", 1)]
        [DataRow(" ", 1)]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Item_DoesntAllow0PriceOrEmptyName(string name, decimal price)
        {
            _ = new Item(name, price);
        }
    }
}
