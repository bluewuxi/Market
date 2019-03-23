using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Market.Lib
{
    public class CartItemRepository
    {
        public IDictionary<string, CartItemEntity> items { get; protected set; }

        public CartItemRepository()
        {
            items = new Dictionary<string, CartItemEntity>();
        }

        public IDictionary<string, CartItemEntity> Empty()
        {
            items.Clear();
            return items;
        }

        public CartItemEntity Add(string itemCode)
        {
            var searchItem = items.Where(u => u.Key == itemCode).Select(p => p.Value).SingleOrDefault();

            // If the product is already in items, simply increase the quantity. Otherwise add a new item.
            if (searchItem == null)
            {
                var newItem = new CartItemEntity(itemCode);
                items.Add(newItem.Code, newItem);
                return newItem;
            }
            else
                searchItem.Increase();
            return searchItem;
        }

    }
}
