namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class archive : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArchiveFilesModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FilePath = c.String(),
                        ArchiveId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArchiveModels", t => t.ArchiveId)
                .Index(t => t.ArchiveId);
            
            CreateTable(
                "dbo.ArchiveModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        EventId = c.Int(),
                        ActionDate = c.DateTime(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.EventModels", t => t.EventId)
                .Index(t => t.AuthorId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArchiveFilesModels", "ArchiveId", "dbo.ArchiveModels");
            DropForeignKey("dbo.ArchiveModels", "EventId", "dbo.EventModels");
            DropForeignKey("dbo.ArchiveModels", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.ArchiveModels", new[] { "EventId" });
            DropIndex("dbo.ArchiveModels", new[] { "AuthorId" });
            DropIndex("dbo.ArchiveFilesModels", new[] { "ArchiveId" });
            DropTable("dbo.ArchiveModels");
            DropTable("dbo.ArchiveFilesModels");
        }
    }
}
