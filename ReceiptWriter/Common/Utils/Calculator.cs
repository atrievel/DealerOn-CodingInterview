using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utils
{
    public class Calculator : ICalculator
    {
        private readonly decimal _taxRate;
        private readonly decimal _importTaxrate;
        private readonly int _conversion;   // used to round to the nearest x cent

        public Calculator(decimal taxRate, decimal importTaxRate, decimal roundToPosition)
        {
            _taxRate = taxRate;
            _importTaxrate = importTaxRate;

            if(roundToPosition != 0)
            {
                _conversion = (int)(1 / Math.Abs(roundToPosition));
            }
            else
            {
                throw new ArgumentException("The round to position cannot be 0.");
            }
        }

        /// <summary>
        /// Calculate taxes at the line item level
        /// </summary>
        /// <param name="lineItems"></param>
        /// <returns>The original list of line items with their calculated taxes</returns>
        public IList<LineItem> CalculateLineItemTaxes(IList<LineItem> lineItems)
        {
            List<LineItem> lineItemsWithTaxes = new List<LineItem>(lineItems.Count);

            foreach (var lineItem in lineItems)
            {
                if (lineItem.IsTaxable)
                {
                    lineItem.Taxes += Math.Ceiling(lineItem.Cost * _taxRate * _conversion) / _conversion;
                }

                if(lineItem.IsImported)
                {
                    lineItem.Taxes += Math.Ceiling(lineItem.Cost * _importTaxrate * _conversion) / _conversion;
                }

                lineItemsWithTaxes.Add(lineItem);
            }

            return lineItemsWithTaxes;
        }

        /// <summary>
        /// Get a sum of all line item taxes
        /// </summary>
        /// <param name="lineItems"></param>
        /// <returns>The sum of all line items' taxes</returns>
        public decimal CalculateReceiptTaxes(IList<LineItem> lineItems)
        {
            return lineItems.Sum(l => l.Taxes);
        }

        /// <summary>
        /// Get the sum of all line items costs + taxes
        /// </summary>
        /// <param name="lineItems"></param>
        /// <returns>The sum of all line items' totals (including taxes)</returns>
        public decimal CalculateReceiptTotal(IList<LineItem> lineItems)
        {
            return lineItems.Sum(l => l.BaseCost);
        }
    }
}