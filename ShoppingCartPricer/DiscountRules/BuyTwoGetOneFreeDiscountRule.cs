using System;
using System.Collections.Generic;

namespace ShoppingCartPricer.DiscountRules
{
    public class BuyTwoGetOneFreeDiscountRule : IDiscountRule
    {
        public decimal CalculateDiscount(IEnumerable<BasketEntry> entries)
        {
            decimal totalDiscount = 0;
            foreach (var entry in entries)
            {
                decimal numberOfTimesToDiscountItem = Math.Floor((decimal)entry.Quantity / 2);
                decimal discountPerItem = entry.Item.Price;
                decimal totalDiscountForEntry = numberOfTimesToDiscountItem * discountPerItem;

                totalDiscount += totalDiscountForEntry;
            }
            return totalDiscount;
        }
    }
}
