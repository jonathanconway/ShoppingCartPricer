using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartPricer.DiscountRules;

namespace ShoppingCartPricer.Tests.DiscountRules
{
    [TestClass]
    public class BuyTwoGetOneFreeDiscountRuleTests
    {
        [TestMethod]
        public void CalculateDiscount_WithoutAnyEntries_ReturnsNoDiscount()
        {
            var testRule = new BuyTwoGetOneFreeDiscountRule();
            var basketEntries = new List<BasketEntry>();

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(0, discount);
        }

        [TestMethod]
        public void CalculateDiscount_WithoutQualifyingEntries_ReturnsNoDiscount()
        {
            var testRule = new BuyTwoGetOneFreeDiscountRule();
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test1", price: 1), quantity: 1),
                new BasketEntry(new Item(name: "Test2", price: 2), quantity: 1),
            });

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(0, discount);
        }

        [TestMethod]
        public void CalculateDiscount_WithSingleQualifyingEntry_ReturnsCorrectDiscount()
        {
            var testRule = new BuyTwoGetOneFreeDiscountRule();
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test1", price: 1), quantity: 2),
            });

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(1, discount);
        }

        [TestMethod]
        public void CalculateDiscount_WithMultipleQualifyingEntries_ReturnsCorrectDiscount()
        {
            var testRule = new BuyTwoGetOneFreeDiscountRule();
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test1", price: 1), quantity: 3),
                new BasketEntry(new Item(name: "Test2", price: 2), quantity: 3),
            });

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(3, discount);
        }
    }
}
