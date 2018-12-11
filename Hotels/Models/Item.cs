using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotels.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual ServiceProduct ServiceProduct { get; set; }
    }
}