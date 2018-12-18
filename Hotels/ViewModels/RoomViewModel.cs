using Hotels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class RoomViewModel
    {
        [Required]
        public string Name { get; set; }

        public int RoomType { get; set; }

        public IEnumerable<RoomType> RoomTypes { get; set; }
    }
}