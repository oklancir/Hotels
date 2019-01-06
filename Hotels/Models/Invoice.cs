using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

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

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
    }
}