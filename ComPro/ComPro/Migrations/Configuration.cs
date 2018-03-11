namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ComPro.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ComPro.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ComPro.Models.ApplicationDbContext context)
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

            // SeedUser(context);
            SeedRole(context);
            //SeedUserRole(context);
            //SeedNotice(context);
            

        }
        private void SeedUserRole(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            var user = context.
                Users.Where(x => x.Email == "pori468@gmail.com").FirstOrDefault();

            manager.AddToRole(user.Id, "Administrator");
        }
        private void SeedRole(ApplicationDbContext context)
        {
            if (!context.Roles.Any(x => x.Name == "Administrator"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Administrator" };

                manager.Create(role);
            }
            if (!context.Roles.Any(x => x.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }
            if (!context.Roles.Any(x => x.Name == "NewUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "NewUser" };

                manager.Create(role);
            }
        }
        private void SeedUser(ApplicationDbContext context)
        {
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "reza"))
            {
                var user1 = new ApplicationUser
                {
                    UserName = "reza",
                    Email = "reza.hoque@outlook.com",
                    PasswordHash = PasswordHash.HashPassword("123456")
                };
                context.Users.Add(user1);
            }
            if (!context.Users.Any(u => u.UserName == "rashid"))
            {
                var user2 = new ApplicationUser
                {
                    UserName = "rashid",
                    Email = "rashid142@gmail.com",
                    PasswordHash = PasswordHash.HashPassword("123456")
                };
                context.Users.Add(user2);
            }

            context.SaveChanges();


        }
        private void SeedNotice(ApplicationDbContext context)
        {
            if (context.Notice.Count() < 20)
            {
                var notice1 = new Models.NoticeBoard
                {
                    Title = "Programming workshop.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ut facilisis mi, eu hendrerit arcu. Praesent consequat justo vel lacinia fermentum. Suspendisse volutpat, ligula et iaculis consequat, nisi mi bibendum orci, vitae semper elit ante at ligula. Praesent pulvinar tortor sed lorem vehicula eleifend. Proin vel dolor volutpat, laoreet lorem et, interdum felis. Curabitur scelerisque, justo ut vestibulum convallis, sapien mi scelerisque risus, at pulvinar neque arcu ut neque. Nulla in felis sem. In quis nisl tellus. In urna mi, molestie non blandit ac, dapibus id ipsum. Fusce convallis fringilla bibendum. Vivamus vitae ex et odio tincidunt convallis. Nullam congue mi dolor, et varius massa tincidunt nec. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras et massa eget orci fermentum egestas a at nisl. Suspendisse accumsan diam sed aliquet tempor. Suspendisse potenti.",
                    SubmitDate = DateTime.Now.AddDays(-5),
                    IsApproved = true,
                    ActionDate = DateTime.Now,
                    CreatorId = "c659e759-d6fa-4180-ac3d-9ddf96377777",
                    WebLink = "http://google.com"

                };
                context.Notice.Add(notice1);
                var notice2 = new Models.NoticeBoard
                {
                    Title = "Summer grill party.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ut facilisis mi, eu hendrerit arcu. Praesent consequat justo vel lacinia fermentum. Suspendisse volutpat, ligula et iaculis consequat, nisi mi bibendum orci, vitae semper elit ante at ligula. Praesent pulvinar tortor sed lorem vehicula eleifend. Proin vel dolor volutpat, laoreet lorem et, interdum felis. Curabitur scelerisque, justo ut vestibulum convallis, sapien mi scelerisque risus, at pulvinar neque arcu ut neque. Nulla in felis sem. In quis nisl tellus. In urna mi, molestie non blandit ac, dapibus id ipsum. Fusce convallis fringilla bibendum. Vivamus vitae ex et odio tincidunt convallis. Nullam congue mi dolor, et varius massa tincidunt nec. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras et massa eget orci fermentum egestas a at nisl. Suspendisse accumsan diam sed aliquet tempor. Suspendisse potenti.",
                    SubmitDate = DateTime.Now.AddDays(-3),
                    IsApproved = true,
                    ActionDate = DateTime.Now,
                    CreatorId = "c659e759-d6fa-4180-ac3d-9ddf96377777",
                    WebLink = "http://msn.com"

                };
                context.Notice.Add(notice2);
                var notice3 = new Models.NoticeBoard
                {
                    Title = "Home made cake for any occassion.",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse ut facilisis mi, eu hendrerit arcu. Praesent consequat justo vel lacinia fermentum. Suspendisse volutpat, ligula et iaculis consequat, nisi mi bibendum orci, vitae semper elit ante at ligula. Praesent pulvinar tortor sed lorem vehicula eleifend. Proin vel dolor volutpat, laoreet lorem et, interdum felis. Curabitur scelerisque, justo ut vestibulum convallis, sapien mi scelerisque risus, at pulvinar neque arcu ut neque. Nulla in felis sem. In quis nisl tellus. In urna mi, molestie non blandit ac, dapibus id ipsum. Fusce convallis fringilla bibendum. Vivamus vitae ex et odio tincidunt convallis. Nullam congue mi dolor, et varius massa tincidunt nec. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Cras et massa eget orci fermentum egestas a at nisl. Suspendisse accumsan diam sed aliquet tempor. Suspendisse potenti.",
                    SubmitDate = DateTime.Now.AddDays(-1),
                    IsApproved = true,
                    ActionDate = DateTime.Now,
                    CreatorId = "c659e759-d6fa-4180-ac3d-9ddf96377777",
                    WebLink = "http://apple.com"

                };
                context.Notice.Add(notice3);

                context.SaveChanges();

            }
        }
    }
}
