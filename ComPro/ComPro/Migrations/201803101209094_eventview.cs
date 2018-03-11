namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventViewModels", "Activity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventViewModels", "Activity");
        }
    }
}
