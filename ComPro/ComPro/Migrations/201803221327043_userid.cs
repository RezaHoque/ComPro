namespace ComPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserInfoes", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.UserInfoes", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserInfoes", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserInfoes", name: "UserId", newName: "User_Id");
        }
    }
}
