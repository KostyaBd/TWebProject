namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathCarTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarDBTables", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarDBTables", "ImagePath");
        }
    }
}
