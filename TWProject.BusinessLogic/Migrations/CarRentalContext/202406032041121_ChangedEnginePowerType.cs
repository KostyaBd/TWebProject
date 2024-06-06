namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEnginePowerType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarDBTables", "EnginePower", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarDBTables", "EnginePower", c => c.Int(nullable: false));
        }
    }
}
