using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Market.Lib;

namespace Market.Tests
{
    [TestClass]
    public class TestSetPrice
    {
        [TestMethod]
        public void SetSinglePrice()
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            PrivateObject po = new PrivateObject(terminal);
            PriceRepository priceTable = (PriceRepository)po.GetField("_priceTable");

            PriceEntity priceItem = new PriceEntity("A", 1.25m, 3, 3.00m);
            terminal.SetPricing(new PriceEntity[] { priceItem });
            PriceEntity resultRecord = priceTable.QueryPrice("A");
            Assert.IsTrue(resultRecord.Code== priceItem.Code
                && resultRecord.UnitPrice==priceItem.UnitPrice
                && resultRecord.Volume==priceItem.Volume
                && resultRecord.VolumePrice==priceItem.VolumePrice, 
                "Fail to add a new price record.");
        }

        [TestMethod]
        public void FirstEmptyPriceTableWhenSetPricing()
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            PrivateObject po = new PrivateObject(terminal);
            PriceRepository priceTable = (PriceRepository)po.GetField("_priceTable");

            PriceEntity[] initPriceTable =
            {
                new PriceEntity("A", 1.25m, 3, 3.00m),
                new PriceEntity("B", 4.25m),
                new PriceEntity("C", 1.00m, 6, 5.00m),
                new PriceEntity("D", 0.75m),
            };
            terminal.SetPricing(initPriceTable);

            Assert.AreEqual(priceTable.priceList.Count, 4, "Should have 4 price records.");

            PriceEntity priceItem = new PriceEntity("X", 2.25m, 2, 2.00m);
            terminal.SetPricing(new PriceEntity[] { priceItem });

            Assert.AreEqual(priceTable.priceList.Count, 1, "Should have 1 price records.");
        }

    }
}
