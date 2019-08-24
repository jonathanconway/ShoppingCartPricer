using System.Collections.Generic;

namespace ShoppingCartPricer.DiscountRules
{
    public static class DiscountRuleFactory
    {
        public static ISet<IDiscountRule> CreateAllDiscountRules()
        {
            return new HashSet<IDiscountRule>(new[] {
                new BuyTwoGetOneFreeDiscountRule()
            });
        }
    }
}
