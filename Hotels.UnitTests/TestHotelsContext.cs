using Hotels.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using HoteliApp.UnitTesting.TestDbSets;
using Hotels.UnitTests.TestDbSets;

namespace Hotels.UnitTests
{
    class TestHotelsContext : IHotelsContext
    {

        public DbSet<Guest> Guests { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }

        public TestHotelsContext()
        {
            Guests = new TestGuestDbSet();
            Rooms = new TestRoomDbSet();
            RoomTypes = new TestRoomTypeDbSet();
            Items = new TestItemDbSet();
            Invoices = new TestInvoiceDbSet();
            Reservations = new TestReservationDbSet();
            ReservationStatuses = new TestDbSet<ReservationStatus>();
            ServiceProducts = new TestDbSet<ServiceProduct>();
        }

        public void Dispose()
        { }

        public int SaveChanges()
        {
            return 0;
        }

        public DbEntityEntry Entry(object entity)
        {
            return null;
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return null;
        }
    }
}
