namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMessageTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageRecieveModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecieverID = c.String(),
                        HasRead = c.String(),
                        ReadingDateTime = c.DateTime(),
                        MessageThreadID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageSendingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderID = c.String(),
                        Massage = c.String(nullable: false),
                        Date_Time = c.DateTime(),
                        MessageThreadID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            //DropTable("dbo.ChatModels");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.ChatModels",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SenderID = c.String(),
            //            SenderName = c.String(),
            //            RecieverID = c.String(),
            //            RecieverName = c.String(),
            //            Massage = c.String(nullable: false),
            //            Date_Time = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            DropTable("dbo.MessageSendingModels");
            DropTable("dbo.MessageRecieveModels");
        }
    }
}
