namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldUserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "Expectation", c => c.String());
            AddColumn("dbo.UserInfoes", "Contribution", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "Contribution");
            DropColumn("dbo.UserInfoes", "Expectation");
        }
    }
}
