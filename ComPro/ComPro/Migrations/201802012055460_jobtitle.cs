namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobtitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "CurrentJobTitle", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "CurrentJobTitle", c => c.String());
        }
    }
}
