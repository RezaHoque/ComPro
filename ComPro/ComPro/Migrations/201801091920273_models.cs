namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        WebLink = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                        OriginalPoster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OriginalPoster_Id)
                .Index(t => t.OriginalPoster_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentJobTitle = c.String(),
                        CompanyName = c.String(),
                        Skills = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notices", "OriginalPoster_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfiles", new[] { "User_Id" });
            DropIndex("dbo.Notices", new[] { "OriginalPoster_Id" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Notices");
        }
    }
}
