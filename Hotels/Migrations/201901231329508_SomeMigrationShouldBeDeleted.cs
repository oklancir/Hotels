namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeMigrationShouldBeDeleted : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Guests", newName: "Guest");
            RenameTable(name: "dbo.Invoices", newName: "Invoice");
            RenameTable(name: "dbo.Items", newName: "Item");
            RenameTable(name: "dbo.ServiceProducts", newName: "ServiceProduct");
            RenameTable(name: "dbo.Reservations", newName: "Reservation");
            RenameTable(name: "dbo.Rooms", newName: "Room");
            RenameTable(name: "dbo.RoomTypes", newName: "RoomType");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RoomType", newName: "RoomTypes");
            RenameTable(name: "dbo.Room", newName: "Rooms");
            RenameTable(name: "dbo.Reservation", newName: "Reservations");
            RenameTable(name: "dbo.ServiceProduct", newName: "ServiceProducts");
            RenameTable(name: "dbo.Item", newName: "Items");
            RenameTable(name: "dbo.Invoice", newName: "Invoices");
            RenameTable(name: "dbo.Guest", newName: "Guests");
        }
    }
}
