﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ComPro.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<PublicComment> PublicComments { get; set; }
        public DbSet<NoticeBoard> Notice { get; set; }
        //public DbSet<NoticeModel> Notices { get; set; }
        //public DbSet<User_Information_Model> User_Information { get; set; }
        //public DbSet<UserProfileModel> UserProfiles { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<IdentityUserLogin> UserLogins { get; set; }
        public DbSet<IdentityUserRole> UserRole { get; set; }
        public DbSet<MessageSendingModel> SendMessage { get; set; }
        public DbSet<MessageRecieveModel> RecieveMessage { get; set; }
        public DbSet<EventModel> Event { get; set; }
        public DbSet<EventMember> EventMember { get; set; }
        public DbSet<SiteImage> SiteImages { get; set; }
        public DbSet<User_Feedback_Model> User_Feedback { get; set; }






        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ComPro.Models.User_Approval_Model> User_Approval_Model { get; set; }

        //public System.Data.Entity.DbSet<ComPro.Models.MemberViewModel> MemberViewModels { get; set; }

        //public System.Data.Entity.DbSet<ComPro.Models.DetailViewModel> DetailViewModels { get; set; }

        //public System.Data.Entity.DbSet<ComPro.Models.EventViewModel> EventViewModels { get; set; }
    }
}