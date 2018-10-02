namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEventUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventModels", "UniqueUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventModels", "UniqueUrl");
        }
    }
}
