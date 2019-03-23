using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Market.Lib;
using System.Linq;

namespace Market.Tests
{
    [TestClass]
    public class TestScan
    {
        PriceEntity[] _priceTable;

        public TestScan()
        {
            _priceTable =
                new PriceEntity[]
                {
                    new PriceEntity("A", 10m),
                    new PriceEntity("B", 20m),
                    new PriceEntity("C", 15m, 3, 35)
                };
        }

        [DataTestMethod]
        [DataRow(new string[] { "A", "B", "C" })]
        public void CanAddItemHasPricing(string[] codes)
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            terminal.SetPricing(_priceTable);
            PrivateObject po = new PrivateObject(terminal);
            CartItemRepository cart = (CartItemRepository)po.GetField("_cart");

            foreach (var code in codes)
            {
                Assert.AreEqual(terminal.ScanProduct(code).CurrentItem.Code, code, 
                    "Should be able to add an item with pricing setting");
            }
            Assert.IsTrue(codes.Where(p=> cart.items.Select(i=> i.Code).Contains(p)).Count() == codes.Length,
                $"[{codes}] should be accepted and added to cart.");
        }

        [DataTestMethod]
        [DataRow("E")]
        [DataRow("G")]
        [DataRow("X")]
        public void IgnoreCodeThatHasNoPrice(string code)
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            terminal.SetPricing(_priceTable);
            PrivateObject po = new PrivateObject(terminal);
            CartItemRepository cart = (CartItemRepository)po.GetField("_cart");

            var result = terminal.ScanProduct(code).CurrentItem;
            Assert.IsTrue(result == null && cart.items.Count == 0,
                $"[{code}] should not be prime,since no pricing for it");
        }

        [DataTestMethod]
        [DataRow("A")]
        [DataRow("B")]
        [DataRow("C")]
        public void IgnoreScanWithEmptyPriceTable(string code)
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();

            PrivateObject po = new PrivateObject(terminal);
            CartItemRepository cart = (CartItemRepository)po.GetField("_cart");

            var result = terminal.ScanProduct(code).CurrentItem;
            Assert.IsTrue(result==null && cart.items.Count==0, 
                $"[{code}] should not be accepted because of empty price table");
        }

    }
}
