using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarketLib;

namespace LibTest
{
    [TestClass]
    public class LibUnitTest
    {
        [TestMethod]
        public void TestScan()
        {
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            terminal.ScanProduct("A");
            terminal.ScanProduct("A");

        }
    }
}
