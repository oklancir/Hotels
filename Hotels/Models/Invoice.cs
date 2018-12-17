using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        [Required]
        public bool IsPaid { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}