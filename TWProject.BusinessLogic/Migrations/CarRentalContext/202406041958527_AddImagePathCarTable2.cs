namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathCarTable2 : DbMigration
    {
		public override void Up()
		{
			Sql("UPDATE CarDBTables SET ImagePath = '/images/car_1.jpg' WHERE Mark = 'Ford' AND Model = 'Fusion'");
			Sql("UPDATE CarDBTables SET ImagePath = '/images/car_2.jpg' WHERE Mark = 'Audi' AND Model = 'Q7'");
			Sql("UPDATE CarDBTables SET ImagePath = '/images/car_3.jpg' WHERE Mark = 'Ford' AND Model = 'Focus'");
			Sql("UPDATE CarDBTables SET ImagePath = '/images/car_4.jpg' WHERE Mark = 'BMW' AND Model = '5 Series'");
			Sql("UPDATE CarDBTables SET ImagePath = '/images/car_5.jpg' WHERE Mark = 'Mercedes-Benz' AND Model = 'C-Class'");
		}


		public override void Down()
		{
			Sql("UPDATE CarDBTables SET ImagePath = NULL WHERE Mark = 'Ford' AND Model = 'Fusion'");
			Sql("UPDATE CarDBTables SET ImagePath = NULL WHERE Mark = 'Audi' AND Model = 'Q7'");
			Sql("UPDATE CarDBTables SET ImagePath = NULL WHERE Mark = 'Ford' AND Model = 'Focus'");
			Sql("UPDATE CarDBTables SET ImagePath = NULL WHERE Mark = 'BMW' AND Model = '5 Series'");
			Sql("UPDATE CarDBTables SET ImagePath = NULL WHERE Mark = 'Mercedes-Benz' AND Model = 'C-Class'");
		}

	}
}
