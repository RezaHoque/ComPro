namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyeventdetailmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DetailViewModels", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DetailViewModels", "IsApproved");
        }
    }
}
