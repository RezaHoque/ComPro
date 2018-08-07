namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_feedback_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User_Feedback_Model",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.Int(nullable: false),
                        Message = c.String(),
                        Stadus = c.Boolean(nullable: false),
                        SubmitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User_Feedback_Model");
        }
    }
}
