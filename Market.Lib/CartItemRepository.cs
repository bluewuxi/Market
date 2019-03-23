using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Market.Lib
{
    public class CartItemRepository
    {
        public IList<CartItemEntity> items { get; protected set; }

        public CartItemRepository()
        {
            items = new List<CartItemEntity>();
        }

        public IList<CartItemEntity> Empty()
        {
            items.Clear();
            return items;
        }

        public CartItemEntity Add(string itemCode)
        {
            var searchItem = items.Where(u => u.Code == itemCode).Select(p => p).SingleOrDefault();
            if (searchItem == null)
            {
                var newItem = new CartItemEntity(itemCode);
                items.Add(newItem);
                return newItem;
            }
            else
                searchItem.Increase();
            return searchItem;
        }

    }
}
