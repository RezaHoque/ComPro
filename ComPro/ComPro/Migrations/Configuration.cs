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

        protected override void Seed(ApplicationDbContext context)
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

            SeedUser(context);
            SeedRole(context);
            SeedUserRole(context);
            //SeedNotice(context);
            //SeedEvents(context);
            //SeedCurrentStatus(context);
            //SeedImages(context);


        }
        private void SeedImages(ApplicationDbContext context)
        {

            var img = new SiteImage
            {
                ImagePath = "/Content/images/bellevue-strandbad.jpg",
                Type = "Event",
                TypeId = context.Event.FirstOrDefault(x => x.Title == "Summer Picnic").EventId,
                UploadDate = DateTime.Now,
                UploaderId = context.Event.FirstOrDefault(x => x.Title == "Summer Picnic").CreatorId

            };
            context.SiteImages.Add(img);
            var img1 = new SiteImage
            {
                ImagePath = "/Content/images/moens-klint.jpg",
                Type = "Event",
                TypeId = context.Event.FirstOrDefault(x => x.Title == "Summer tour to Møn").EventId,
                UploadDate = DateTime.Now,
                UploaderId = context.Event.FirstOrDefault(x => x.Title == "Summer tour to Møn").CreatorId

            };
            context.SiteImages.Add(img1);
            context.SaveChanges();
        }
        private void SeedEvents(ApplicationDbContext context)
        {
            var events = new EventModel
            {
                Title = "Pohela boishak 2018",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.",
                Date = new DateTime(2018, 4, 14, 10, 00, 00),
                Creation = DateTime.Now,
                Place = "In a Hall",
                IsApproved = true,
                CreatorId = "5ced017f-6f95-44e7-9c33-261b6172bbe8",
                EventStatus=true,
                ApprovalDate=DateTime.Now

            };
            context.Event.Add(events);

            var events1 = new EventModel
            {
                Title = "Summer Picnic",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.",
                Date = new DateTime(2018, 4, 19, 12, 00, 00),
                Creation = DateTime.Now,
                Place = "Klampenborg beach",
                IsApproved = true,
                CreatorId = "41d04839-4c98-4be2-8521-c403b0fd16d5",
                EventStatus = true,
                ApprovalDate = DateTime.Now

            };
            context.Event.Add(events1);
            var events2 = new EventModel
            {
                Title = "BTH Get together",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.",
                Date = new DateTime(2018, 4, 10, 10, 00, 00),
                Creation = DateTime.Now,
                Place = "In a Hall",
                IsApproved = true,
                CreatorId = "41d04839-4c98-4be2-8521-c403b0fd16d5",
                EventStatus = true,
                ApprovalDate = DateTime.Now

            };
            context.Event.Add(events2);

            var events3 = new EventModel
            {
                Title = "Summer tour to Møn",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.",
                Date = new DateTime(2018, 6, 14, 10, 00, 00),
                Creation = DateTime.Now,
                Place = "Mønsklint",
                IsApproved = true,
                CreatorId = "5ced017f-6f95-44e7-9c33-261b6172bbe8",
                EventStatus = true,
                ApprovalDate = DateTime.Now

            };

            context.Event.Add(events3);

            var events4 = new EventModel
            {
                Title = "Code BD",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce quis lectus quis sem lacinia nonummy. Proin mollis lorem non dolor. In hac habitasse platea dictumst. Nulla ultrices odio.",
                Date = new DateTime(2018, 5, 14, 10, 00, 00),
                Creation = DateTime.Now,
                Place = "In a Hall",
                IsApproved = true,
                CreatorId = "41d04839-4c98-4be2-8521-c403b0fd16d5",
                EventStatus = true,
                ApprovalDate = DateTime.Now

            };

            context.Event.Add(events4);

            context.SaveChanges();
        }
        private void SeedUserRole(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            var user = context.
                Users.Where(x => x.Email == "reza.hoque@outlook.com").FirstOrDefault();

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
            if (!context.Users.Any(u => u.UserName == "reza.hoque@outlook.com"))
            {
                var user1 = new ApplicationUser
                {
                    UserName = "reza.hoque@outlook.com",
                    Email = "reza.hoque@outlook.com",
                    PasswordHash = PasswordHash.HashPassword("123456")
                };
                context.Users.Add(user1);
            }
            //if (!context.Users.Any(u => u.UserName == "rashid"))
            //{
            //    var user2 = new ApplicationUser
            //    {
            //        UserName = "rashid",
            //        Email = "rashid142@gmail.com",
            //        PasswordHash = PasswordHash.HashPassword("123456")
            //    };
            //    context.Users.Add(user2);
            //}

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

        //private void SeedCurrentStatus(ApplicationDbContext context)
        //{
        //    if (!context.CurrentStatus.Any(x => x.Status == "Employeed"))
        //    {
        //        var status = new Models.Current_Status_Model
        //        {
        //            Status = "Employeed"
        //         };
        //    }


        //    if (!context.CurrentStatus.Any(x => x.Status == "Job Seeker"))
        //    {
        //        var status = new Models.Current_Status_Model
        //        {
        //            Status = "Job Seeker"
        //        };
        //    }

        //    if (!context.CurrentStatus.Any(x => x.Status == "Student"))
        //    {
        //        var status = new Models.Current_Status_Model
        //        {
        //            Status = "Student"
        //        };
        //    }
        //}
    }
}
