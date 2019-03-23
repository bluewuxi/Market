using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Market.Lib;

namespace Market.Tests
{
    [TestClass]
    public class TestCaculate
    {
        [TestMethod]
        public void ApplyVolumePriceWhenNumReaches()
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            PriceEntity[] priceTable =
            {
                new PriceEntity("A", 1m, 3, 2.5m),
            };
            decimal firstTotal = terminal.SetPricing(priceTable)
                                    .ScanProduct("A")
                                    .ScanProduct("A")
                                    .CalculateTotal();
            decimal secondTotal = terminal.ScanProduct("A").CalculateTotal();
            Assert.IsTrue(firstTotal==2m && secondTotal==2.5m, "Fail to apply Volume Price.");
        }

        [TestMethod]
        public void ResetCleansList()
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            PrivateObject po = new PrivateObject(terminal);
            CartItemRepository cart = (CartItemRepository)po.GetField("_cart");

            PriceEntity[] priceTable =
            {
                new PriceEntity("A", 1m, 3, 2m),
                new PriceEntity("B", 2m, 5, 2.5m)
            };

            terminal.SetPricing(priceTable).ScanProduct("A").ScanProduct("B");
            Assert.IsTrue(cart.items.Count == 2, "Fail to scan two products.");
            terminal.EmptyCart();
            Assert.IsTrue(cart.items.Count == 0, "Fail to empty cart.");
            
        }
    }
}
