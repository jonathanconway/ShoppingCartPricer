using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartPricer;
using ShoppingCartPricer.DiscountRules;

namespace Tests
{
    /// <summary>
    /// Mock discount rule, which applies a discount of 1 per item quantity.
    /// </summary>
    class MockDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(IEnumerable<BasketEntry> entries)
        {
            decimal totalDiscount = 0;
            foreach (var entry in entries)
            {
                totalDiscount += entry.Quantity;
            }
            return totalDiscount;
        }
    }

    [TestClass]
    public class PriceCalculatorTests
    {
        [TestMethod]
        public void CalculateTotal_WithNoDiscountRules_AndNoItems_ReturnsZeroTotal()
        {
            var discountRules = new HashSet<IDiscountRule>();
            var calculator = new PriceCalculator(discountRules);
            var basketEntries = new List<BasketEntry>();

            var total = calculator.CalculateTotal(basketEntries);

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void CalculateTotal_WithNoDiscountRules_AndSingleEntry_ReturnsCorrectTotal()
        {
            var discountRules = new HashSet<IDiscountRule>();
            var calculator = new PriceCalculator(discountRules);
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test 1", price: 1), quantity: 1)
            });

            var total = calculator.CalculateTotal(basketEntries);

            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void CalculateTotal_WithNoDiscountRules_AndMultipleVaryingEntries_ReturnsCorrectTotal()
        {
            var discountRules = new HashSet<IDiscountRule>();
            var calculator = new PriceCalculator(discountRules);
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test 1", price: 1), quantity: 1),
                new BasketEntry(new Item(name: "Test 2", price: 2), quantity: 2)
            });

            var total = calculator.CalculateTotal(basketEntries);

            Assert.AreEqual(5, total);
        }

        [TestMethod]
        public void CalculateTotal_WithMockDiscountRule_AndNoItems_ReturnsZeroTotal()
        {
            var discountRules = new HashSet<IDiscountRule>(new[] {
                new MockDiscountRule()
            });
            var calculator = new PriceCalculator(discountRules);
            var basketEntries = new List<BasketEntry>();

            var total = calculator.CalculateTotal(basketEntries);

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void CalculateTotal_WithMockDiscountRule_AndDiscountGreaterThanTotalPriceOfEntries_ReturnsNoLessThanZeroTotal()
        {
            var discountRules = new HashSet<IDiscountRule>(new[] {
                new MockDiscountRule()
            });
            var calculator = new PriceCalculator(discountRules);
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test", price: .5M))
            });

            var total = calculator.CalculateTotal(basketEntries);

            Assert.IsFalse(total < 0);
        }

        [TestMethod]
        public void CalculateTotal_WithMockDiscountRule_AndMultipleVaryingEntries_ReturnsCorrectTotal()
        {
            var discountRules = new HashSet<IDiscountRule>(new[] {
                new MockDiscountRule()
            });
            var calculator = new PriceCalculator(discountRules);
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test 1", price: 2), quantity: 2),
                new BasketEntry(new Item(name: "Test 2", price: 2), quantity: 2)
            });

            var total = calculator.CalculateTotal(basketEntries);

            // Price: 2 items * 2 quantity * 2 each = 8.
            // Discount: 1 per item quantity * 2 items * 2 quantity = 4.
            // Total should be 8 - 4 = 4.
            Assert.AreEqual(4, total);
        }
    }
}