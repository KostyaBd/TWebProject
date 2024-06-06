namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCapacity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarDBTables", "EngineCapacity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarDBTables", "EngineCapacity", c => c.Int(nullable: false));
        }
    }
}
