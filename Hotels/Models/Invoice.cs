namespace Hotels.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public double TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}