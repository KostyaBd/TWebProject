namespace TWProject.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialReCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UDBTables", "level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UDBTables", "level");
        }
    }
}
