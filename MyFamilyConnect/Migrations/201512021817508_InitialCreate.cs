namespace MyFamilyConnect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        NewsPhotoItemId = c.Int(nullable: false),
                        Text = c.String(),
                        UserProfileId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.NewsPhotoItems", t => t.NewsPhotoItemId, cascadeDelete: true)
                .Index(t => t.NewsPhotoItemId);
            
            CreateTable(
                "dbo.NewsPhotoItems",
                c => new
                    {
                        NewsPhotoItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        HasPhoto = c.Boolean(nullable: false),
                        Photo = c.Binary(),
                        UserProfileId = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NewsPhotoItemId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthdate = c.DateTime(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Email = c.String(),
                        AboutMe = c.String(),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "NewsPhotoItemId", "dbo.NewsPhotoItems");
            DropIndex("dbo.Comments", new[] { "NewsPhotoItemId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.NewsPhotoItems");
            DropTable("dbo.Comments");
        }
    }
}
