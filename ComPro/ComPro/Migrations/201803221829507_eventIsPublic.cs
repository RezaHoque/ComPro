namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventIsPublic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventModels", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventModels", "IsPublic");
        }
    }
}
