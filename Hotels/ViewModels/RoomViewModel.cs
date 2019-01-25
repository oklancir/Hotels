using Hotels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hotels.ViewModels
{
    public class RoomViewModel
    {
        [Required, Display(Name = "Room Name")]
        public string Name { get; set; }

        public int RoomTypeId { get; set; }

        [Required, Display(Name = "Room Type")]
        public RoomType RoomType { get; set; }

        public IEnumerable<RoomType> RoomTypes { get; set; }
    }
}