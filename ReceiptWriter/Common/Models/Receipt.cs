
using System.Collections.Generic;

namespace Common
{
    public class Receipt
    {
        public IList<LineItem> LineItems { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}