using System;
using System.Collections.Generic;

namespace ShoppingCartPricer.DiscountRules
{
    public class TenPercentOffInDecemberDiscountRule : IDiscountRule
    {
        const int DECEMBER = 12;

        private DateTime _systemNowDateTime;

        public TenPercentOffInDecemberDiscountRule()
        {
            this._systemNowDateTime = DateTime.Now;
        }

        public TenPercentOffInDecemberDiscountRule(DateTime systemNowDateTime)
        {
            this._systemNowDateTime = systemNowDateTime;
        }

        public decimal CalculateDiscount(IEnumerable<BasketEntry> entries)
        {
            decimal totalDiscount = 0;
            if (this._systemNowDateTime.Month == DECEMBER)
            {
                foreach (var entry in entries)
                {
                    totalDiscount += (entry.Quantity * entry.Item.Price) / 10;
                }
            }
            return totalDiscount;
        }
    }
}
