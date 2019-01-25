namespace Hotels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRoomModelAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Room", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Room", "Name", c => c.String(nullable: false));
        }
    }
}
