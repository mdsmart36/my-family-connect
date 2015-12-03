namespace MyFamilyConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "UserProfileId", c => c.Int());
            AlterColumn("dbo.NewsPhotoItems", "UserProfileId", c => c.Int());
            CreateIndex("dbo.Comments", "UserProfileId");
            CreateIndex("dbo.NewsPhotoItems", "UserProfileId");
            AddForeignKey("dbo.Comments", "UserProfileId", "dbo.UserProfiles", "UserProfileId");
            AddForeignKey("dbo.NewsPhotoItems", "UserProfileId", "dbo.UserProfiles", "UserProfileId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsPhotoItems", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Comments", "UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.NewsPhotoItems", new[] { "UserProfileId" });
            DropIndex("dbo.Comments", new[] { "UserProfileId" });
            AlterColumn("dbo.NewsPhotoItems", "UserProfileId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "UserProfileId", c => c.Int(nullable: false));
        }
    }
}
