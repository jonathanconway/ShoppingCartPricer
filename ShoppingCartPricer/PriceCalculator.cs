using System;
using System.Collections.Generic;
using ShoppingCartPricer.DiscountRules;

namespace ShoppingCartPricer
{
    /// <summary>
    /// Calculates prices and applies discounts to entries of items in the basket.
    /// </summary>
    public class PriceCalculator
    {
        private readonly ISet<IDiscountRule> _discountRules;

        public PriceCalculator(ISet<IDiscountRule> discountRules)
        {
            _discountRules = discountRules;
        }

        public decimal CalculateTotal(IEnumerable<BasketEntry> basketEntries)
        {
            var totalPrice = CalculateTotalPriceByQuantity(basketEntries);
            var totalDiscount = CalculateTotalDiscount(basketEntries);
            var total = SubtractDiscountFromPrice(totalPrice, totalDiscount);

            return total;
        }

        private decimal CalculateTotalPriceByQuantity(IEnumerable<BasketEntry> basketEntries)
        {
            decimal total = 0;
            foreach (var entry in basketEntries)
            {
                total += entry.Item.Price * entry.Quantity;
            }
            return total;
        }

        private decimal CalculateTotalDiscount(IEnumerable<BasketEntry> basketEntries)
        {
            decimal totalDiscount = 0;
            foreach (var discountRule in _discountRules)
            {
                totalDiscount += discountRule.CalculateDiscount(basketEntries);
            }
            return totalDiscount;
        }

        private decimal SubtractDiscountFromPrice(decimal totalPrice, decimal totalDiscount)
        {
            var total = totalPrice - totalDiscount;

            // Total should never fall below zero.
            total = Math.Max(0, total);
            return total;
        }

    }
}
