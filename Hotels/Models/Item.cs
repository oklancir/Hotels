using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Item
    {
        [Display(Name = "Item Id")]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual ServiceProduct ServiceProduct { get; set; }
    }
}