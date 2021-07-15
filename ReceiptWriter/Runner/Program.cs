using System;
using Common.IO;
using Common.Utils;
using Common;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // setup input file location and config variables
            string filePath = GetFilePathFromUser();
            IConfigurationRoot configuration = SetupConfig();

            Receipt receipt = new();
            IDataReader reader = new JsonDataReader(filePath);
            IReceiptWriter writer = new ConsoleReceiptWriter();
            ICalculator calculator = new Calculator(
                decimal.Parse(configuration["taxRate"]),
                decimal.Parse(configuration["importTaxRate"]),
                decimal.Parse(configuration["roundTo"]));

            // get data then calculate all taxes and totals
            IList<LineItem> lineItems = await reader.ReadDataAsync();

            if(lineItems.Count > 0)
            {
                receipt.LineItems = calculator.CalculateLineItemTaxes(lineItems);
                receipt.Tax = calculator.CalculateReceiptTaxes(receipt.LineItems);
                receipt.Total = calculator.CalculateReceiptTotal(receipt.LineItems);

                // populate the console with the desired output
                writer.WriteReceipt(receipt);
            }

        }

        /// <summary>
        /// Prompt the user for a file location, which will be used as the input for the application
        /// </summary>
        /// <returns>The file location as a string, or exists the program if the user cancels the input</returns>
        private static string GetFilePathFromUser()
        {
            string filePath = "";

            while(filePath.Length < 1)
            {
                Console.WriteLine("Enter a file path or the letter 'c' to cancel:");
                filePath = Console.ReadLine();

                if(filePath.Length > 0 && filePath.ToLower().Equals("c"))
                {
                    Environment.Exit(0);
                }
                else if(filePath.Length > 0 && !File.Exists(filePath))
                {
                    Console.WriteLine("The filepath you entered does not exist. Please try another.\n");
                    filePath = "";
                }
            }

            return filePath;
        }

        private static IConfigurationRoot SetupConfig()
        {
            return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}