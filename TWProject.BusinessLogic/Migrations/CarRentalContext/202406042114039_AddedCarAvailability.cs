namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCarAvailability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarDBTables", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarDBTables", "IsAvailable");
        }
    }
}
