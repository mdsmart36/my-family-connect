namespace MyFamilyConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "NewsPhotoItem_NewsPhotoItemId", "dbo.NewsPhotoItems");
            DropForeignKey("dbo.NewsPhotoItems", "UserProfile_UserProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Comments", new[] { "NewsPhotoItem_NewsPhotoItemId" });
            DropIndex("dbo.NewsPhotoItems", new[] { "UserProfile_UserProfileId" });
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        UserProfile_UserProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_UserProfileId)
                .Index(t => t.UserProfile_UserProfileId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        Content = c.Binary(),
                        TimeStamp = c.DateTime(nullable: false),
                        UserProfile_UserProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_UserProfileId)
                .Index(t => t.UserProfile_UserProfileId);
            
            AddColumn("dbo.Comments", "NewsItem_NewsId", c => c.Int());
            AddColumn("dbo.Comments", "PhotoItem_PhotoId", c => c.Int());
            CreateIndex("dbo.Comments", "NewsItem_NewsId");
            CreateIndex("dbo.Comments", "PhotoItem_PhotoId");
            AddForeignKey("dbo.Comments", "NewsItem_NewsId", "dbo.News", "NewsId");
            AddForeignKey("dbo.Comments", "PhotoItem_PhotoId", "dbo.Photos", "PhotoId");
            DropColumn("dbo.Comments", "NewsPhotoItem_NewsPhotoItemId");
            DropTable("dbo.NewsPhotoItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NewsPhotoItems",
                c => new
                    {
                        NewsPhotoItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        HasPhoto = c.Boolean(nullable: false),
                        Photo = c.Binary(),
                        TimeStamp = c.DateTime(nullable: false),
                        UserProfile_UserProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.NewsPhotoItemId);
            
            AddColumn("dbo.Comments", "NewsPhotoItem_NewsPhotoItemId", c => c.Int());
            DropForeignKey("dbo.Photos", "UserProfile_UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Comments", "PhotoItem_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.News", "UserProfile_UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Comments", "NewsItem_NewsId", "dbo.News");
            DropIndex("dbo.Photos", new[] { "UserProfile_UserProfileId" });
            DropIndex("dbo.News", new[] { "UserProfile_UserProfileId" });
            DropIndex("dbo.Comments", new[] { "PhotoItem_PhotoId" });
            DropIndex("dbo.Comments", new[] { "NewsItem_NewsId" });
            DropColumn("dbo.Comments", "PhotoItem_PhotoId");
            DropColumn("dbo.Comments", "NewsItem_NewsId");
            DropTable("dbo.Photos");
            DropTable("dbo.News");
            CreateIndex("dbo.NewsPhotoItems", "UserProfile_UserProfileId");
            CreateIndex("dbo.Comments", "NewsPhotoItem_NewsPhotoItemId");
            AddForeignKey("dbo.NewsPhotoItems", "UserProfile_UserProfileId", "dbo.UserProfiles", "UserProfileId");
            AddForeignKey("dbo.Comments", "NewsPhotoItem_NewsPhotoItemId", "dbo.NewsPhotoItems", "NewsPhotoItemId");
        }
    }
}
