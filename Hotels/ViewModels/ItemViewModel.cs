using Hotels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class ItemViewModel
    {
        public int Amount { get; set; }

        public int ServiceProduct { get; set; }

        public int Invoice { get; set; }

        public IEnumerable<ServiceProduct> ServiceProducts { get; set; }
    }
}