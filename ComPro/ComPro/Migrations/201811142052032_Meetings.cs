namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Meetings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meetings_Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Creator_Id = c.String(),
                        Creator_Name = c.String(),
                        Creation_Date = c.DateTime(nullable: false),
                        Meeting_Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Perticipents_Id = c.String(),
                        Perticipents_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Meetings_Models");
        }
    }
}
