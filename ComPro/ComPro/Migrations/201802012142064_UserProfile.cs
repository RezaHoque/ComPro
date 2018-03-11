namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Name", c => c.String(nullable: false));
            AddColumn("dbo.UserProfiles", "Photo", c => c.String());
            AddColumn("dbo.UserProfiles", "Phone", c => c.Int(nullable: false));
            AddColumn("dbo.UserProfiles", "Gender", c => c.String(nullable: false));
            AddColumn("dbo.UserProfiles", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserProfiles", "Email", c => c.String(nullable: false));
            AddColumn("dbo.UserProfiles", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.UserProfiles", "ConfirmPassword", c => c.String());
            AlterColumn("dbo.UserProfiles", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "Skills", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "PostCode", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "City", c => c.String());
            AlterColumn("dbo.UserProfiles", "PostCode", c => c.String());
            AlterColumn("dbo.UserProfiles", "Address", c => c.String());
            AlterColumn("dbo.UserProfiles", "Skills", c => c.String());
            AlterColumn("dbo.UserProfiles", "CompanyName", c => c.String());
            DropColumn("dbo.UserProfiles", "ConfirmPassword");
            DropColumn("dbo.UserProfiles", "Password");
            DropColumn("dbo.UserProfiles", "Email");
            DropColumn("dbo.UserProfiles", "BirthDate");
            DropColumn("dbo.UserProfiles", "Gender");
            DropColumn("dbo.UserProfiles", "Phone");
            DropColumn("dbo.UserProfiles", "Photo");
            DropColumn("dbo.UserProfiles", "Name");
        }
    }
}
