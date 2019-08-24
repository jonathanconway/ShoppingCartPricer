using System;

namespace ShoppingCartPricer
{
    public class BasketEntry
    {
        public Item Item { get; }
        public int Quantity { get; set; }

        public BasketEntry(Item item, int quantity = 1)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException();
            }

            Item = item;
            Quantity = quantity;
        }
    }
}
