namespace Core.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public decimal Value { get; set; }
        public enum State {Pending , Success , Failed }
    }
}