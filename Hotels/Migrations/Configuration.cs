using Hotels.Models;
using System.Collections.Generic;

namespace Hotels.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Hotels.Models.HotelsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hotels.Models.HotelsContext context)
        {
            var roomTypes = new List<RoomType>()
            {
                new RoomType()  { Name = "Single bed", Price = 15000},
                new RoomType()  { Name = "Double bed", Price = 25000},
                new RoomType()  { Name = "Triple bed", Price = 35000},
                new RoomType()  { Name = "Penthouse", Price = 150000}
            };

            foreach (var roomType in roomTypes)
            {
                context.RoomTypes.AddOrUpdate(rt => rt.Name, roomType);
            }
        }
    }
}
