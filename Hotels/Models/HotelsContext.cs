using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hotels.Models
{
    public class HotelsContext : DbContext
    {
        public HotelsContext() : base("name=HotelsDbContext")
        {
            
        }
    }
}