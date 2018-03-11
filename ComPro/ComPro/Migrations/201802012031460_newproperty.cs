namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Address", c => c.String());
            AddColumn("dbo.UserProfiles", "PostCode", c => c.String());
            AddColumn("dbo.UserProfiles", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "City");
            DropColumn("dbo.UserProfiles", "PostCode");
            DropColumn("dbo.UserProfiles", "Address");
        }
    }
}
