namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_information : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Photo = c.String(),
                        Address = c.String(),
                        PostCode = c.Int(nullable: false),
                        City = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Gender = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        CurrentJobTitle = c.String(nullable: false),
                        Skills = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_Information");
        }
    }
}
