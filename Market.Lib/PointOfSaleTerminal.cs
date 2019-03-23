using System;
using System.Collections.Generic;

namespace Market.Lib
{
    public class PointOfSaleTerminal
    {
        public CartItemEntity CurrentItem { get; protected set; }
        public PriceEntity CurrentItemPricing { get; protected set; }
        PriceRepository _priceTable;
        CartItemRepository _cart;

        public PointOfSaleTerminal()
        {
            _priceTable = new PriceRepository();
            _cart = new CartItemRepository();
            CurrentItem = null;
            CurrentItemPricing = null;
        }

        public PointOfSaleTerminal SetPricing(IList<PriceEntity> priceList)
        {
            _priceTable.Empty();
            foreach (var singlePricing in priceList)
            {
                _priceTable.SetSinglePricing(singlePricing.Code, singlePricing.UnitPrice, singlePricing.Volume, singlePricing.VolumePrice);
            }
            return this;
        }

        public PointOfSaleTerminal ScanProduct(string code)
        {
            var pricing = _priceTable.QueryPrice(code);

            // Only add the product if it has price info.
            if (pricing != null)
            {
                CurrentItem = _cart.Add(code);
                CurrentItemPricing = pricing;
            }
            return this;
        }

        public decimal CalculateTotal()
        {
            decimal total = 0m;
            foreach(var item in _cart.items)
            {
                total += _priceTable.QueryPrice(item.Code).Caculate(item.Count);
            }
            return total;
        }

        public PointOfSaleTerminal EmptyCart()
        {
            _cart.Empty();
            CurrentItem = null;
            CurrentItemPricing = null;
            return this;
        }
    }
}

