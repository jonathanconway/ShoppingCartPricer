using System;

namespace ShoppingCartPricer
{
    public class Item
    {
        public string Name { get; }
        public decimal Price { get; }

        public Item(string name, decimal price)
        {
            if (price <= 0 || name.Trim() == "")
            {
                throw new ArgumentException();
            }

            Name = name;
            Price = price;
        }
    }
}
