using System.Collections.Generic;

namespace Common.IO
{
    public interface IReceiptWriter
    {
        void WriteReceipt(Receipt receipt);
        IDictionary<string, LineItem> GroupLineItems(IList<LineItem> lineItems);
    }
}