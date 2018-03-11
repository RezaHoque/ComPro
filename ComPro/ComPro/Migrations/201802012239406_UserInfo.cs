namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Photo = c.String(),
                        Address = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Gender = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        CurrentJobTitle = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        Skills = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserInfoes", new[] { "User_Id" });
            DropTable("dbo.UserInfoes");
        }
    }
}
