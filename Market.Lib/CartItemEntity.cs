using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Lib
{
    public class CartItemEntity
    {
        public string Code { get; set; }
        public int Count { get; set; }

        public CartItemEntity(string code)
        {
            Code = code;
            Count = 1;
        }

        public void Increase (int amount=1)
        {
            Count += amount;
        }
    }
}
