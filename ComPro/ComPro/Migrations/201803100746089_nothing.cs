namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nothing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventViewModels", "Approval", c => c.Boolean(nullable: false));
            AddColumn("dbo.EventViewModels", "CreatorID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventViewModels", "CreatorID");
            DropColumn("dbo.EventViewModels", "Approval");
        }
    }
}
