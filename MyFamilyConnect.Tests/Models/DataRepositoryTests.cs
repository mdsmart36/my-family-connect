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
    }
}
