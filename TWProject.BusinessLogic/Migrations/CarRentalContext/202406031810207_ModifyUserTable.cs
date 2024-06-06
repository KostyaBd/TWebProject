namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UDBTables", "DateReceived");
            DropColumn("dbo.UDBTables", "DateReturned");
            DropColumn("dbo.UDBTables", "DaysRented");
            DropColumn("dbo.UDBTables", "TotalPaid");
            DropColumn("dbo.UDBTables", "IsVerified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UDBTables", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.UDBTables", "TotalPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.UDBTables", "DaysRented", c => c.Int(nullable: false));
            AddColumn("dbo.UDBTables", "DateReturned", c => c.DateTime(nullable: false));
            AddColumn("dbo.UDBTables", "DateReceived", c => c.DateTime(nullable: false));
        }
    }
}
