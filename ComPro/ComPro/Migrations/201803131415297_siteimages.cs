namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class siteimages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        TypeId = c.Int(nullable: false),
                        Type = c.String(),
                        UploaderId = c.String(maxLength: 128),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UploaderId)
                .Index(t => t.UploaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteImages", "UploaderId", "dbo.AspNetUsers");
            DropIndex("dbo.SiteImages", new[] { "UploaderId" });
            DropTable("dbo.SiteImages");
        }
    }
}
