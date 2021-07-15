

using System;
using System.Collections.Generic;

namespace Common.IO
{
    public class ConsoleReceiptWriter : IReceiptWriter
    {
        public ConsoleReceiptWriter()
        {
        }

        /// <summary>
        ///  Display all line items, taxes, and totals for the receipt
        /// </summary>
        /// <param name="receipt"></param>
        public void WriteReceipt(Receipt receipt)
        {
            foreach (var lineItem in GroupLineItems(receipt.LineItems).Values)
            {
                Console.WriteLine(lineItem.ToString());
            }

            Console.WriteLine($"Sales tax: {receipt.Tax:F2}");
            Console.WriteLine($"Total: {receipt.Total:F2}");
        }

        /// <summary>
        /// Group the line items based on name and price
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public IDictionary<string, LineItem> GroupLineItems(IList<LineItem> lineItems)
        {
            IDictionary<string, LineItem> uniqueLineItems = new Dictionary<string, LineItem>();

            foreach (var lineItem in lineItems)
            {
                string uniqueKey = $"{lineItem.Name}-{lineItem.Cost}";

                if (uniqueLineItems.ContainsKey(uniqueKey))
                {
                    uniqueLineItems[uniqueKey].Quantity += 1;
                }
                else
                {
                    uniqueLineItems.TryAdd(uniqueKey, lineItem);
                }
            }

            return uniqueLineItems;
        }
    }
}
