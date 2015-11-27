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
        private List<NewsPhotoItem> my_list;
        private ApplicationUser user1, user2, user3;

        private void ConnectMocksToDataSource()
        {
            // This setups the Mocks and connects to the Data Source (my_list in this case)
            var data = my_list.AsQueryable();

            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_NewsPhotos.As<IQueryable<NewsPhotoItem>>().Setup(m => m.Expression).Returns(data.Expression);

            mock_context.Setup(m => m.NewsAndPhotos).Returns(mock_NewsPhotos.Object);
            // This allows BoardRepository to call Boards.Add and have it update the my_list instance and Enumerator
            // Connect DbSet.Add to List.Add so they work together
            mock_NewsPhotos.Setup(m => m.Add(It.IsAny<NewsPhotoItem>())).Callback((NewsPhotoItem b) => my_list.Add(b));
            mock_NewsPhotos.Setup(m => m.Remove(It.IsAny<NewsPhotoItem>())).Callback((NewsPhotoItem b) => my_list.Remove(b));
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<DataContext>();
            mock_NewsPhotos = new Mock<DbSet<NewsPhotoItem>>();
            my_list = new List<NewsPhotoItem>();            
            user1 = new ApplicationUser();
            user2 = new ApplicationUser();
            user3 = new ApplicationUser();
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_NewsPhotos = null;
            my_list = null;
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

            ConnectMocksToDataSource();

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

            ConnectMocksToDataSource();

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
            ConnectMocksToDataSource();
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

            ConnectMocksToDataSource();

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
            ConnectMocksToDataSource();
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

            ConnectMocksToDataSource();

            // Act                        
            data_repo.AddNewsPhotoItem(news_item1);
            data_repo.AddNewsPhotoItem(news_item2);
            NewsPhotoItem news = data_repo.DeleteNewsPhotoItem(news_item1);

            // Assert
            Assert.IsNotNull(news);
            Assert.AreEqual(news.UserProfileId, news_item1.UserProfileId);
            Assert.AreEqual(1, data_repo.GetAllNewsPhotosCount());
        }
    }
}