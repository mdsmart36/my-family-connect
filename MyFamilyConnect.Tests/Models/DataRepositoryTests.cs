using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using MyFamilyConnect.Models;
using Moq;

namespace MyFamilyConnect.Tests.Models
{
    [TestClass]
    public class DataRepositoryTests
    {
        private Mock<DataContext> mock_context;
        private Mock<DbSet<NewsPhotoItem>> mock_NewsPhotos;
        private Mock<DbSet<UserProfile>> mock_UserProfiles;
        private Mock<DbSet<Comment>> mock_Comments;
        private List<NewsPhotoItem> news_list;
        private List<UserProfile> profile_list;
        private List<Comment> comment_list;
        //private ApplicationUser user1, user2, user3;

        private void ConnectNewsMocksToDataSource()
        {
            // This setups the Mocks and connects to the Data Source (news_list in this case)
            var data = news_list.AsQueryable();

            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.NewsAndPhotos).Returns(mock_NewsPhotos.Object);
            // This allows BoardRepository to call Boards.Add and have it update the news_list instance and Enumerator
            // Connect DbSet.Add to List.Add so they work together
            mock_NewsPhotos.Setup(m => m.Add(It.IsAny<NewsPhotoItem>())).Callback((NewsPhotoItem b) => news_list.Add(b));
            mock_NewsPhotos.Setup(m => m.Remove(It.IsAny<NewsPhotoItem>())).Callback((NewsPhotoItem b) => news_list.Remove(b));
        }

        private void ConnectProfileMocksToDataSource()
        {
            // This setups the Mocks and connects to the Data Source (profile_list in this case)
            var data = profile_list.AsQueryable();

            mock_UserProfiles.As<IQueryable<UserProfile>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_UserProfiles.As<IQueryable<UserProfile>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mock_UserProfiles.As<IQueryable<UserProfile>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_UserProfiles.As<IQueryable<UserProfile>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.UserProfiles).Returns(mock_UserProfiles.Object);            
            mock_UserProfiles.Setup(m => m.Add(It.IsAny<UserProfile>())).Callback((UserProfile b) => profile_list.Add(b));
            mock_UserProfiles.Setup(m => m.Remove(It.IsAny<UserProfile>())).Callback((UserProfile b) => profile_list.Remove(b));
        }

        private void ConnectCommentMocksToDataSource()
        {
            // This setups the Mocks and connects to the Data Source (comment_list in this case)
            var data = comment_list.AsQueryable();

            mock_Comments.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_Comments.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mock_Comments.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_Comments.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.Comments).Returns(mock_Comments.Object);
            mock_Comments.Setup(m => m.Add(It.IsAny<Comment>())).Callback((Comment b) => comment_list.Add(b));
            mock_Comments.Setup(m => m.Remove(It.IsAny<Comment>())).Callback((Comment b) => comment_list.Remove(b));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<DataContext>();
            mock_NewsPhotos = new Mock<DbSet<NewsPhotoItem>>();
            mock_UserProfiles = new Mock<DbSet<UserProfile>>();
            mock_Comments = new Mock<DbSet<Comment>>();
            news_list = new List<NewsPhotoItem>();
            profile_list = new List<UserProfile>();
            comment_list = new List<Comment>();
            //user1 = new ApplicationUser();
            //user2 = new ApplicationUser();
            //user3 = new ApplicationUser();
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_NewsPhotos = null;
            news_list = null;
            profile_list = null;
            comment_list = null;
        }

        [TestMethod]
        public void DataRepoEnsureCanCreateInstance()
        {
            DataRepository data_repo = new DataRepository(mock_context.Object);
            Assert.IsNotNull(data_repo);
        }

        [TestMethod]
        public void DataRepoCanCreateNewsPhotoItem()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            NewsPhotoItem news_item1 = new NewsPhotoItem()
            {
                Title = "title",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 2,
                UserProfileId = 1,
                Text = "news text"
            };
            NewsPhotoItem news_item2 = new NewsPhotoItem()
            {
                Title = "title",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 2,
                UserProfileId = 1,
                Text = "news text"
            };

            ConnectNewsMocksToDataSource();

            // Act                        
            bool actual = data_repo.AddNewsPhotoItem(news_item1);
            // Assert
            Assert.AreEqual(1, data_repo.GetAllNewsPhotosCount());
            Assert.IsTrue(actual);
            // Act
            actual = false;            
            actual = data_repo.AddNewsPhotoItem(news_item2);
            // Assert
            Assert.AreEqual(2, data_repo.GetAllNewsPhotosCount());
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DataRepoCanGetAllNewsPhotoItems()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            NewsPhotoItem news_item1 = new NewsPhotoItem()
            {
                Title = "title 1",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 1,
                UserProfileId = 1,
                Text = "news text 1"
            };
            NewsPhotoItem news_item2 = new NewsPhotoItem()
            {
                Title = "title 2",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 2,
                UserProfileId = 1,
                Text = "news text 2"
            };

            ConnectNewsMocksToDataSource();

            // Act           
            data_repo.AddNewsPhotoItem(news_item1);
            data_repo.AddNewsPhotoItem(news_item2);
            int expected = 2;
            int actual = data_repo.GetAllNewsPhotoItems().Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRepoEnsureZeroNewsPhotoItems()
        {
            // Arrange            
            ConnectNewsMocksToDataSource();
            DataRepository data_repo = new DataRepository(mock_context.Object);

            // Act
            int expected = 0;
            int actual = data_repo.GetAllNewsPhotosCount();
            // Assert
            Assert.AreEqual(expected, actual);
        }       

        [TestMethod]
        public void DataRepoCanGetNewsPhotoItemForUser()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            NewsPhotoItem news_item1 = new NewsPhotoItem()
            {
                Title = "title 1",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 1,
                UserProfileId = 1,
                Text = "news text 1"
            };
            NewsPhotoItem news_item2 = new NewsPhotoItem()
            {
                Title = "title 2",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 2,
                UserProfileId = 2,
                Text = "news text 2"
            };

            ConnectNewsMocksToDataSource();

            // Act
            data_repo.AddNewsPhotoItem(news_item1);
            data_repo.AddNewsPhotoItem(news_item2);
            int expected = 1;
            int actual = data_repo.GetNewsPhotosForUser(news_item2.UserProfileId).Count;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DataRepoCanUpdateNewsPhotoItemTitle()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            NewsPhotoItem news_item1 = new NewsPhotoItem()
            {
                Title = "title 1",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 1,
                UserProfileId = 1,
                Text = "news text 1"
            };
            ConnectNewsMocksToDataSource();
            data_repo.AddNewsPhotoItem(news_item1);

            // Act
            string expected = "new title";
            var actual = data_repo.UpdateNewsPhotoTitle(news_item1.UserProfileId, news_item1.Title, expected);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DataRepoCanDeleteNewsPhotoItem()
        {
            DataRepository data_repo = new DataRepository(mock_context.Object);
            NewsPhotoItem news_item1 = new NewsPhotoItem()
            {
                Title = "title",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 2,
                UserProfileId = 1,
                Text = "news text"
            };
            NewsPhotoItem news_item2 = new NewsPhotoItem()
            {
                Title = "title",
                HasPhoto = false,
                Comments = null,
                NewsPhotoId = 2,
                UserProfileId = 1,
                Text = "news text"
            };

            ConnectNewsMocksToDataSource();

            // Act                        
            data_repo.AddNewsPhotoItem(news_item1);
            data_repo.AddNewsPhotoItem(news_item2);
            NewsPhotoItem news = data_repo.DeleteNewsPhotoItem(news_item1);

            // Assert
            Assert.IsNotNull(news);
            Assert.AreEqual(news.UserProfileId, news_item1.UserProfileId);
            Assert.AreEqual(1, data_repo.GetAllNewsPhotosCount());
        }

        [TestMethod]
        public void DataRepoCreateUserProfile()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            UserProfile profile = new UserProfile { UserId = 1, FirstName = "Matt", LastName = "Smart" };
            ConnectProfileMocksToDataSource();
            
            // Act
            var success = data_repo.AddUserProfile(profile);

            // Assert
            Assert.IsTrue(success);
            Assert.AreEqual(1, data_repo.GetProfileCount());
        }

        [TestMethod]
        public void DataRepoReadUserProfile()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            UserProfile profile1 = new UserProfile { UserId = 1, FirstName = "Matt", LastName = "Smart" };
            UserProfile profile2 = new UserProfile { UserId = 2, FirstName = "Sally", LastName = "Smart" };
            ConnectProfileMocksToDataSource();

            // Act
            data_repo.AddUserProfile(profile1);
            data_repo.AddUserProfile(profile2);
            UserProfile found = data_repo.GetUserProfile(profile1.UserId);

            // Assert
            Assert.AreEqual(found.FirstName, profile1.FirstName);

        }

        [TestMethod]
        public void DataRepoUpdateUserProfile()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            UserProfile profile1 = new UserProfile { UserId = 1, FirstName = "Matt", LastName = "Smart" };
            UserProfile profile2 = new UserProfile { UserId = 2, FirstName = "Sally", LastName = "Smart" };
            ConnectProfileMocksToDataSource();

            // Act
            data_repo.AddUserProfile(profile1);
            data_repo.AddUserProfile(profile2);
            string newName = "Hannah";
            data_repo.UpdateUserProfile(profile2.UserId, newName);
            var found = data_repo.GetUserProfile(profile2.UserId);

            // Assert
            Assert.AreEqual(newName, found.FirstName);
        }

        [TestMethod]
        public void DataRepoDeleteUserProfile()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            UserProfile profile1 = new UserProfile { UserId = 1, FirstName = "Matt", LastName = "Smart" };
            UserProfile profile2 = new UserProfile { UserId = 2, FirstName = "Sally", LastName = "Smart" };
            UserProfile profile3 = new UserProfile { UserId = 3, FirstName = "Micah", LastName = "Smart" };
            ConnectProfileMocksToDataSource();

            // Act
            data_repo.AddUserProfile(profile1);
            data_repo.AddUserProfile(profile2);
            data_repo.AddUserProfile(profile3);
            var success = data_repo.DeleteUserProfile(profile1.UserId);
            success = data_repo.DeleteUserProfile(profile2.UserId);
            var found = data_repo.GetUserProfile(profile2.UserId);

            // Assert
            Assert.IsNull(found); // cannot find profile which has been deleted
            Assert.IsTrue(success); // successfully deleted a profile
            Assert.AreEqual(1, data_repo.GetAllUserProfiles().Count); // only 1 profile left
        }

        [TestMethod]
        public void DataRepoCreateComment()
        {
            // Arrange
            DataRepository data_repo = new DataRepository(mock_context.Object);
            Comment comment1 = new Comment { CommentId = 1, Text = "this is what i had to say", UserProfileId = 1 };
            Comment comment2 = new Comment { CommentId = 2, Text = "I wish I had more to say", UserProfileId = 1 };
            ConnectCommentMocksToDataSource();

            // Act
            var success1 = data_repo.AddComment(comment1);
            var success2 = data_repo.AddComment(comment2);

            // Assert
            Assert.IsTrue(success1);
            Assert.IsTrue(success2);
            Assert.AreEqual(2, data_repo.GetAllComments().Count());
        }

        [TestMethod]
        public void DataRepoReadComment()
        {

        }

        [TestMethod]
        public void DataRepoUpdateComment()
        {

        }

        [TestMethod]
        public void DataRepoDeleteComment()
        {

        }
    }
}