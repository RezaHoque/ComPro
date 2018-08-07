namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User_Feedback_Model", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.User_Feedback_Model", "Stadus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Feedback_Model", "Stadus", c => c.Boolean(nullable: false));
            DropColumn("dbo.User_Feedback_Model", "Status");
        }
    }
}
