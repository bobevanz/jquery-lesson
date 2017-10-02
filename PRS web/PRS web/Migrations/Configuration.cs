namespace PRS_web.Migrations
{
    using PRS_web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PRS_web.Models.PRS_dbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(PRS_web.Models.PRS_dbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Users.AddOrUpdate(
                u => u.UserName,
                    new User { UserName = "Admin", Password ="Admin", FirstName="System", LastName="Admin",Phone="513-264-2574",Email="bobevans547@gmail.com", IsReviewer=true, IsAdmin=true }
                );
        }
    }
}
