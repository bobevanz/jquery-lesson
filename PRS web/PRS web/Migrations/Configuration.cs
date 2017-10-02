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
                    new User { UserName = "Admin", Password = "Admin",
                        FirstName = "System", LastName = "Admin",
                        Phone = "513-264-2574", Email = "bobevans547@gmail.com",
                        IsReviewer = true, IsAdmin = true },
                    new User {UserName = "User",Password = "User",
                        FirstName = "Normal",LastName = "User",
                        Phone = "513-555-5555", Email = "emailemail@gmail.com",
                        IsReviewer = true,
                        IsAdmin = true}
                                       
                );
            context.Vendors.AddOrUpdate(
                v => v.Name,
                    new Vendor
                    {
                        Code = "AMA",
                        Name = "Amazon",
                        Address = "123 Street Way",
                        City = "Hebron",
                        State = "KY",
                        Zip = "45000",
                        Phone = "513-867-5309",
                        Email = "amazonemail@gmail.com",
                        IsPreApproved = true
                    },
                    
                    new Vendor{
                        Code = "WAL",
                        Name = "Walmart",
                        Address = "234 gravel street",
                        City = "Someplace",
                        State = "KA",
                        Zip = "21485",
                        Phone = "857-414-6984",
                        Email = "walmartemail@gmail.com",
                        IsPreApproved = true
                    },
                    new Vendor{
                        Code = "TAR",
                        Name = "Target",
                        Address = "567 sunshine lane",
                        City = "Anywhere",
                        State = "CA",
                        Zip = "71864",
                        Phone = "963-696-2598",
                        Email = "targetemail@gmail.com",
                        IsPreApproved = true
                    }
                );
        }

    }
}
