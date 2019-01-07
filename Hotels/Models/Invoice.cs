using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public bool IsPaid { get; set; } = false;

        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}