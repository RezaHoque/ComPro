namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventend : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventModels", "End", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventModels", "End");
        }
    }
}
