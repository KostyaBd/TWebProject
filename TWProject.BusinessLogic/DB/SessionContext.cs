using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.User;

namespace TWProject.BusinessLogic.DB
{
    internal class SessionContext : DbContext
    {
        public SessionContext() : base("CarRental")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SessionContext>());
        }
        public virtual DbSet<Session> Sessions { get; set; }
    }
}
