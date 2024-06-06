namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCarTable : DbMigration
    {
		public override void Up()
		{
			Sql("INSERT INTO CarDBTables (Mark, Model, ProductionYear, BodyType, SeatsNum, Color, Odometer, EnginePower, EngineCapacity, PricePerDay, GearboxType, FuelType, ImagePath) " +
				"VALUES ('Ford', 'Fusion', 2018, 9, 5, 'White', 79883, 175, 2, 150, 0, 3, '/images/car_1.jpg')");
			Sql("INSERT INTO CarDBTables (Mark, Model, ProductionYear, BodyType, SeatsNum, Color, Odometer, EnginePower, EngineCapacity, PricePerDay, GearboxType, FuelType, ImagePath) " +
				"VALUES ('Audi', 'Q7', 2011, 4, 7, 'Black', 86853, 333, 3, 130, 0, 0, '/images/car_2.jpg')");
			Sql("INSERT INTO CarDBTables (Mark, Model, ProductionYear, BodyType, SeatsNum, Color, Odometer, EnginePower, EngineCapacity, PricePerDay, GearboxType, FuelType, ImagePath) " +
				"VALUES ('Ford', 'Focus', 2015, 12, 5, 'White', 56037, 100, 1.6, 110, 1, 0, '/images/car_3.jpg')");
			Sql("INSERT INTO CarDBTables (Mark, Model, ProductionYear, BodyType, SeatsNum, Color, Odometer, EnginePower, EngineCapacity, PricePerDay, GearboxType, FuelType, ImagePath) " +
				"VALUES ('BMW', '5 Series', 2021, 9, 5, 'Blue', 23452, 288, 2, 300, 0, 0, '/images/car_4.jpg')");
			Sql("INSERT INTO CarDBTables (Mark, Model, ProductionYear, BodyType, SeatsNum, Color, Odometer, EnginePower, EngineCapacity, PricePerDay, GearboxType, FuelType, ImagePath) " +
				"VALUES ('Mercedes-Benz', 'C-Class', 2022, 9, 5, 'Gray', 18451, 255, 2, 300, 0, 0, '/images/car_5.jpg')");
		}

		public override void Down()
		{
			Sql("DELETE FROM CarDBTables WHERE Mark = 'Ford'");
			Sql("DELETE FROM CarDBTables WHERE Mark = 'Audi'");
			Sql("DELETE FROM CarDBTables WHERE Mark = 'Ford'");
			Sql("DELETE FROM CarDBTables WHERE Mark = 'BMW'");
			Sql("DELETE FROM CarDBTables WHERE Mark = 'Mercedes-Benz'");
		}
	}
}
