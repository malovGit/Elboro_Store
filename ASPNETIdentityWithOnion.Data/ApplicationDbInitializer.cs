using System;
using System.Data.Entity;
using ASPNETIdentityWithOnion.Core.DomainModels;
using ASPNETIdentityWithOnion.Core.Logging;
using ASPNETIdentityWithOnion.Data.Identity;
using ASPNETIdentityWithOnion.Data.Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ASPNETIdentityWithOnion.Core.DomainModels.StoreModels;

namespace ASPNETIdentityWithOnion.Data
{
    // This is useful if you do not want to tear down the database each time you run the application.
     //public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<AspnetIdentityWithOnionContext>
    {
        protected override void Seed(AspnetIdentityWithOnionContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public void InitializeIdentityForEF(AspnetIdentityWithOnionContext db)
        {
            // This is only for testing purpose
            const string name = "admin@admin.com";
            const string password = "Admin@123456";
            string[] roleName = new string[] { "Admin", "Anonim", "Manager", "User" };
            var applicationRoleManager = IdentityFactory.CreateRoleManager(db);
            var applicationUserManager = IdentityFactory.CreateUserManager(db);
            //Create Role Admin if it does not exist
            
            ApplicationIdentityRole role;
            foreach (var item in roleName)
            {
                role = new ApplicationIdentityRole { Name = item };
                applicationRoleManager.Create(role);
                role = null;
            }
            //var role = applicationRoleManager.FindByName(roleName);
            //if (role == null)
            //{
            //    role = new ApplicationIdentityRole { Name = roleName };
            //    applicationRoleManager.Create(role);
            //}

            var user = applicationUserManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationIdentityUser { UserName = name, Email = name };
                applicationUserManager.Create(user, password);
                applicationUserManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = applicationUserManager.GetRoles(user.Id);
            foreach (var item in roleName)
            {
                if (!rolesForUser.Contains(item))
                {
                    applicationUserManager.AddToRole(user.Id, item);
                }
            }
            
            var context = new AspnetIdentityWithOnionContext("name=AppContext", new DebugLogger());
            //var image = new Image { Path = "http://lorempixel.com/400/200/" };
            //context.Set<Image>().Add(image);

            //context.Set<Category>().Add(new Category { Name = "Cameras", IsActive = true, Path = "6a81942d-d825-4c18-97ca-4a5a906c68ad.jpg" });
            //context.Set<Category>().Add(new Category { Name = "BagsCase", IsActive = true, Path = "49307dd0-7ada-4e12-b1e9-e5040269288f.jpg" });
            //context.Set<Category>().Add(new Category { Name = "Accessuare", IsActive = true, Path = "1c68860a-3b9a-4278-90ce-0b0237ec4898.jpg" });
            
            //context.Set<SubCategory>().Add(new SubCategory { Name = "SLR Cameras", })
            //for (var i = 0; i < 10; i++)
            //{
            //    context.Set<Product>().Add(new Product { Name = "My Product", Price = 35});

            //}
            //context.Set<Category>().Add(new Category { Name = "ForExample", IsActive = true });
            //context.SaveChanges();
        }
        class DebugLogger : ILogger
        {
            public void Log(string message)
            {
                
            }

            public void Log(Exception ex)
            {
                
            }
        }
    }
}