namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuserinfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "Address", c => c.String());
            AlterColumn("dbo.UserInfoes", "PostCode", c => c.String());
            AlterColumn("dbo.UserInfoes", "City", c => c.String());
            AlterColumn("dbo.UserInfoes", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.UserInfoes", "CurrentJobTitle", c => c.String());
            AlterColumn("dbo.UserInfoes", "CompanyName", c => c.String());
            AlterColumn("dbo.UserInfoes", "Skills", c => c.String());
            DropColumn("dbo.UserInfoes", "Password");
            DropColumn("dbo.UserInfoes", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "ConfirmPassword", c => c.String());
            AddColumn("dbo.UserInfoes", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.UserInfoes", "Skills", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "CurrentJobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserInfoes", "City", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "PostCode", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "Address", c => c.String(nullable: false));
        }
    }
}
