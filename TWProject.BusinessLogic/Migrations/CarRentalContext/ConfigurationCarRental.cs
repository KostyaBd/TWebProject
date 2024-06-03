namespace TWProject.BusinessLogic.Migrations.CarRentalContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationCarRental : DbMigrationsConfiguration<TWProject.BusinessLogic.DB.CarRentalContext>
    {
        public ConfigurationCarRental()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\CarRentalContext";
        }

        protected override void Seed(TWProject.BusinessLogic.DB.CarRentalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
