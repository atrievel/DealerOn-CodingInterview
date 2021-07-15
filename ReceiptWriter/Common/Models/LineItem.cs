using System.Text.Json.Serialization;

namespace Common
{
    public class LineItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsImported { get; set; }
        public decimal BaseCost { get { return Quantity * Cost + Taxes * Quantity; } }
        public decimal Taxes { get; set; } = 0;

        public override string ToString()
        {
            return $"{Name.Trim()}: {BaseCost:F2}" +
                $"{(Quantity > 1 ? string.Format(" ({0} @ {1:F2})", Quantity, BaseCost/Quantity ): string.Empty)}";
        }
    }
}