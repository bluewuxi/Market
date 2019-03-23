using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Market.Lib;
using System.Collections.Generic;
using System.Linq;

namespace Market.Tests
{
    [TestClass]
    public class TestSampleCases
    {
        PointOfSaleTerminal terminal;

        public TestSampleCases()
        {
            terminal = new PointOfSaleTerminal();
            PriceEntity[] priceTable = 
            {
                new PriceEntity("A", 1.25m, 3, 3.00m),
                new PriceEntity("B", 4.25m),
                new PriceEntity("C", 1.00m, 6, 5.00m),
                new PriceEntity("D", 0.75m),
            };
            terminal.SetPricing(priceTable);

        }

        public static T[] Shuffle<T>(IList<T> deck) where T : ICloneable
        {
            T[] disorderedDeck = deck.Select(i => (T)i.Clone()).ToArray<T>();
            Random r = new Random();
            for (int n = disorderedDeck.Count() - 1; n > 0; --n)
            {
                int k = r.Next(n + 1);
                T temp = disorderedDeck[n];
                disorderedDeck[n] = disorderedDeck[k];
                disorderedDeck[k] = temp;
            }
            return disorderedDeck;
        }

        [DataTestMethod]
        [DataRow(new string[] { "A", "B", "C", "D", "A", "B", "A" }, 13.25)]
        [DataRow(new string[] { "C", "C", "C", "C", "C", "C", "C" }, 6)]
        [DataRow(new string[] { "A", "B", "C", "D" }, 7.25)]
        public void TestGivenCases(string[] codes, double expectedResult)
        {
            foreach (var code in codes) terminal.ScanProduct(code);
            decimal result = terminal.CalculateTotal();

            Assert.IsTrue(result == (decimal)expectedResult, $"Should be {expectedResult} while got {result}");
        }

        [DataTestMethod]
        [DataRow(new string[] { "A", "B", "C", "D", "A", "B", "A" }, 13.25)]
        [DataRow(new string[] { "C", "C", "C", "C", "C", "C", "C" }, 6)]
        [DataRow(new string[] { "A", "B", "C", "D" }, 7.25)]
        public void TestGivenCasesWithDifferentOrder(string[] codes, double expectedResult)
        {
            string[] disorderedCodes = TestSampleCases.Shuffle<string>(codes);
            foreach (var code in disorderedCodes) terminal.ScanProduct(code);
            decimal result = terminal.CalculateTotal();
            Assert.IsTrue(result == (decimal)expectedResult, $"Should be {expectedResult} while got {result}");
        }

    }
}
