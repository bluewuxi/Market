using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Lib
{
    public class PriceEntity
    {
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public int? Volume { get; set; }
        public decimal? VolumePrice { get; set; }

        public PriceEntity(string code, decimal unitPrice, int? volume=null, decimal? volumePrice=null)
        {
            Code = code;
            UnitPrice = unitPrice;
            Volume = volume;
            VolumePrice = volumePrice;
        }

        public decimal Caculate( int itemCount)
        {
            // If no volume price set, use unit price directly.
            if (Volume == null || VolumePrice == null) return UnitPrice * itemCount;

            int packageCount = (int)(itemCount / Volume);
            return (decimal)(UnitPrice * (itemCount % Volume) + packageCount * VolumePrice);
        }
    }
}
