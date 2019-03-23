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
        string outputBuffer = "";
        decimal total = 0m;

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

            Reset();
        }

        private void Output(string message, string footer="", bool reset=false)
        {
            if (reset)
            {
                textDisplay.Text = "";
                outputBuffer = "";
            }
            if (message == "") return;

            outputBuffer += message + "\r\n";
            textDisplay.Text = outputBuffer + footer;
            textDisplay.Select(textDisplay.Text.Length, 0);
            textDisplay.ScrollToCaret();
        }

        private void Reset()
        {
            terminal.EmptyCart();
            total = 0m;
            Output("Ready for scan\t============================", "", true);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            string code =((Button)sender).Text;
            terminal.ScanProduct(code);

            decimal previousTotal = total;
            total = terminal.CalculateTotal();

            decimal differ = previousTotal + terminal.CurrentItemPricing.UnitPrice - total;

            // The non-zero differ means volume pricing has been applied.
            // If differ exists, print the discount.
            if (differ!=0m)
            {
                Output($" {code}\t\t\t ${terminal.CurrentItemPricing.UnitPrice} ");
                Output($" \t{terminal.CurrentItemPricing.Volume} for ${terminal.CurrentItemPricing.VolumePrice}\t-${differ} ",
                    $"\t------------------------------------\r\n\t\t Total:\t ${total}");
            }
            else
            {
                Output($" {code}\t\t\t ${terminal.CurrentItemPricing.UnitPrice} ",
                    $"\t------------------------------------\r\n\t\t Total:\t ${total}");
            }

        }


        //// Now we caculate at real time.
        //private void buttonCaculate_Click(object sender, EventArgs e)
        //{
        //    Output("-----------------------");
        //    Output($"Total: ${terminal.CalculateTotal()}" );
        //}
    }
}
