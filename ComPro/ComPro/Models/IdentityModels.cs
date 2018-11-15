using System.Data.Entity;
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
        public DbSet<SiteContibuter> SiteContibuters { get; set; }
        public DbSet<SiteFeaturePlan> SiteFeaturePlans { get; set; }
        //public DbSet<Current_Status_Model> CurrentStatus { get; set; }
        public DbSet<ArchiveModel> Archives { get; set; }
        public DbSet<ArchiveFilesModel> ArchiveFiles { get; set; }
        public DbSet<FeedModel> Feeds { get; set; }

        public DbSet<PollingAndSyrvayModel> PollingAndSyrvays { get; set; }
        public DbSet<QuestionModel> Questions { get; set; }
        public DbSet<AnswerModel> Answers { get; set; }
        public DbSet<PerticipentModel> Perticipents { get; set; }

        public DbSet<Meetings_Models> Meetings_Models { get; set; }





        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        }
}