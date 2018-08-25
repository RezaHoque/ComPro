namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current_Status : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Current_Status_Model",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Current_Status_Model");
        }
    }
}
