namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingDBTables",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        BookingRecievedDate = c.DateTime(nullable: false),
                        BookingReturnDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Car_CarId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.CarDBTables", t => t.Car_CarId)
                .ForeignKey("dbo.UDBTables", t => t.User_UserId)
                .Index(t => t.Car_CarId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.CarDBTables",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Mark = c.String(),
                        Model = c.String(),
                        ProductionYear = c.Int(nullable: false),
                        BodyType = c.Int(nullable: false),
                        SeatsNum = c.Int(nullable: false),
                        Color = c.String(),
                        Odometer = c.Int(nullable: false),
                        EnginePower = c.Int(nullable: false),
                        EngineCapacity = c.Int(nullable: false),
                        PricePerDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GearboxType = c.Int(nullable: false),
                        FuelType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
            CreateTable(
                "dbo.UDBTables",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(nullable: false),
                        DaysRented = c.Int(nullable: false),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsVerified = c.Boolean(nullable: false),
                        level = c.Int(nullable: false),
                        Car_CarId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.CarDBTables", t => t.Car_CarId)
                .Index(t => t.Car_CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingDBTables", "User_UserId", "dbo.UDBTables");
            DropForeignKey("dbo.UDBTables", "Car_CarId", "dbo.CarDBTables");
            DropForeignKey("dbo.BookingDBTables", "Car_CarId", "dbo.CarDBTables");
            DropIndex("dbo.UDBTables", new[] { "Car_CarId" });
            DropIndex("dbo.BookingDBTables", new[] { "User_UserId" });
            DropIndex("dbo.BookingDBTables", new[] { "Car_CarId" });
            DropTable("dbo.UDBTables");
            DropTable("dbo.CarDBTables");
            DropTable("dbo.BookingDBTables");
        }
    }
}
