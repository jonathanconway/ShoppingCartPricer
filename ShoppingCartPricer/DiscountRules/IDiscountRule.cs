using System;
using System.Collections.Generic;

namespace ShoppingCartPricer.DiscountRules
{
    public interface IDiscountRule
    {
        decimal CalculateDiscount(IEnumerable<BasketEntry> entries);
    }
}
