using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class ServiceProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}