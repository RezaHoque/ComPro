namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current_status_moved : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.Current_Status_Model");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.Current_Status_Model",
            //    c => new
            //        {
            //            StatusId = c.Int(nullable: false, identity: true),
            //            Status = c.String(),
            //        })
            //    .PrimaryKey(t => t.StatusId);
            
        }
    }
}
