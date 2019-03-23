using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Market.Lib
{
    public class PriceRepository
    {
        public IList<PriceEntity> priceList { get; protected set; }

        public PriceRepository()
        {
            priceList = new List<PriceEntity>();
        }

        public IList<PriceEntity> Empty()
        {
            priceList.Clear();
            return priceList;
        }

        public PriceEntity SetSinglePricing(string code, decimal unitPrice, int? volume = null, decimal? volumePrice = null)
        {
            var searchPricing = QueryPrice(code);
            if (searchPricing == null)
            {
                var newPricing = new PriceEntity(code, unitPrice, volume, volumePrice);
                priceList.Add(newPricing);
                return newPricing;
            }
            else
            {
                searchPricing.UnitPrice = unitPrice;
                searchPricing.Volume = volume;
                searchPricing.VolumePrice = volumePrice;
                return searchPricing;
            }
        }

        public PriceEntity QueryPrice(string code)
        {
            return priceList.Where(u => u.Code == code).Select(p => p).SingleOrDefault();
        }

    }
}
