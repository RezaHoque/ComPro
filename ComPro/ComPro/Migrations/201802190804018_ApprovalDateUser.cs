namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApprovalDateUser : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.NoticeModels", "OriginalPoster_Id", "dbo.AspNetUsers");
            //DropForeignKey("dbo.UserProfileModels", "User_Id", "dbo.AspNetUsers");
            //DropIndex("dbo.NoticeModels", new[] { "OriginalPoster_Id" });
            //DropIndex("dbo.UserProfileModels", new[] { "User_Id" });
            //CreateTable(
            //    "dbo.ChatModels",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SenderID = c.String(),
            //            SenderName = c.String(),
            //            RecieverID = c.String(),
            //            RecieverName = c.String(),
            //            Massage = c.String(nullable: false),
            //            Date_Time = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserInfoes", "ApprovalDate", c => c.DateTime());
            //DropTable("dbo.NoticeModels");
            //DropTable("dbo.User_Information_Model");
            //DropTable("dbo.UserProfileModels");
        }
        
        public override void Down()
        {
        //    CreateTable(
        //        "dbo.UserProfileModels",
        //        c => new
        //        {
        //            Id = c.Int(nullable: false, identity: true),
        //            CurrentJobTitle = c.String(nullable: false),
        //            CompanyName = c.String(nullable: false),
        //            Skills = c.String(nullable: false),
        //            Address = c.String(nullable: false),
        //            PostCode = c.String(nullable: false),
        //            City = c.String(nullable: false),
        //            Name = c.String(nullable: false),
        //            Photo = c.String(),
        //            Phone = c.Int(nullable: false),
        //            Gender = c.String(nullable: false),
        //            BirthDate = c.DateTime(nullable: false),
        //            Email = c.String(nullable: false),
        //            Password = c.String(nullable: false, maxLength: 100),
        //            ConfirmPassword = c.String(),
        //            User_Id = c.String(maxLength: 128),
        //        })
        //        .PrimaryKey(t => t.Id);

        //    CreateTable(
        //        "dbo.User_Information_Model",
        //        c => new
        //            {
        //                Id = c.Int(nullable: false, identity: true),
        //                Name = c.String(nullable: false),
        //                Photo = c.String(),
        //                Address = c.String(),
        //                PostCode = c.Int(nullable: false),
        //                City = c.String(nullable: false),
        //                Phone = c.Int(nullable: false),
        //                Gender = c.String(nullable: false),
        //                BirthDate = c.DateTime(nullable: false),
        //                CurrentJobTitle = c.String(nullable: false),
        //                Skills = c.String(nullable: false),
        //                CompanyName = c.String(nullable: false),
        //                Email = c.String(nullable: false),
        //                Password = c.String(nullable: false, maxLength: 100),
        //                ConfirmPassword = c.String(),
        //            })
        //        .PrimaryKey(t => t.Id);
            
        //    CreateTable(
        //        "dbo.NoticeModels",
        //        c => new
        //            {
        //                Id = c.Int(nullable: false, identity: true),
        //                Title = c.String(),
        //                Description = c.String(),
        //                WebLink = c.String(),
        //                IsApproved = c.Boolean(nullable: false),
        //                ActionDate = c.DateTime(nullable: false),
        //                SubmitDate = c.DateTime(nullable: false),
        //                OriginalPoster_Id = c.String(maxLength: 128),
        //            })
        //        .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.UserInfoes", "ApprovalDate");
            ////DropTable("dbo.ChatModels");
            //CreateIndex("dbo.UserProfileModels", "User_Id");
            //CreateIndex("dbo.NoticeModels", "OriginalPoster_Id");
            //AddForeignKey("dbo.UserProfileModels", "User_Id", "dbo.AspNetUsers", "Id");
            //AddForeignKey("dbo.NoticeModels", "OriginalPoster_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
