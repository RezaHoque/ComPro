namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contributors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteContibuters",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ContributingUserId = c.String(maxLength: 128),
                        Contribution = c.String(),
                        ActionDate = c.DateTime(nullable: false),
                        PhotoPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ContributingUserId)
                .Index(t => t.ContributingUserId);
            
            CreateTable(
                "dbo.SiteFeaturePlans",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FeatureDetails = c.String(),
                        IsOngoing = c.Boolean(nullable: false),
                        ContributingUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ContributingUserId)
                .Index(t => t.ContributingUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteFeaturePlans", "ContributingUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SiteContibuters", "ContributingUserId", "dbo.AspNetUsers");
            DropIndex("dbo.SiteFeaturePlans", new[] { "ContributingUserId" });
            DropIndex("dbo.SiteContibuters", new[] { "ContributingUserId" });
            DropTable("dbo.SiteFeaturePlans");
            DropTable("dbo.SiteContibuters");
        }
    }
}
