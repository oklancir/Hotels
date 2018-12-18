using Hotels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        public int Amount { get; set; }

        [Required]
        public int ServiceProduct { get; set; }

        public int Invoice { get; set; }

        public IEnumerable<ServiceProduct> ServiceProducts { get; set; }
    }
}