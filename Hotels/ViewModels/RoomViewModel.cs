using Hotels.Models;
using System.Collections.Generic;

namespace Hotels.ViewModels
{
    public class RoomViewModel
    {
        public string Name { get; set; }

        public int RoomType { get; set; }

        public IEnumerable<RoomType> RoomTypes { get; set; }
    }
}