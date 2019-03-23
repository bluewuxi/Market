using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Market.Lib;

namespace ConsolePanel
{
    public partial class CheckConsole : Form
    {
        PointOfSaleTerminal terminal = new PointOfSaleTerminal();

        public CheckConsole()
        {
            InitializeComponent();
        }

        private void Console_Load(object sender, EventArgs e)
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

        private void buttonReset_Click(object sender, EventArgs e)
        {
            terminal.EmptyCart();
            textDisplay.Text = "";
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            string code =((Button)sender).Text;
            terminal.ScanProduct(code);
            textDisplay.Text += $"Scan {code} \r\n";
            textDisplay.Select(textDisplay.Text.Length,0);
            textDisplay.ScrollToCaret();
        }

        private void buttonCaculate_Click(object sender, EventArgs e)
        {
            textDisplay.Text += "-----------------------\r\n";
            textDisplay.Text += "Total: " + terminal.CalculateTotal() + "\r\n";
            textDisplay.Select(textDisplay.Text.Length, 0);
            textDisplay.ScrollToCaret();
        }
    }
}
