using System.Collections.Generic;

namespace Common.Utils
{
    public interface ICalculator
    {
        IList<LineItem> CalculateLineItemTaxes(IList<LineItem> lineItems);
        decimal CalculateReceiptTotal(IList<LineItem> lineItems);
        decimal CalculateReceiptTaxes(IList<LineItem> lineItems);
    }
}