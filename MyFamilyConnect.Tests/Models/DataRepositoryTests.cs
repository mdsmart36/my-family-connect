using Moq;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFamilyConnect.Models;


namespace MyFamilyConnect.Tests.Models
{
    [TestClass]
    public class DataRepositoryTests
    {
        private Mock<DataContext> mock_context;
        private Mock<DbSet<NewsPhotoItem>> mock_NewsPhotos;
        

        private void ConnectMocksToDataSource()
        {
            // This setups the Mocks and connects to the Data Source (my_list in this case)
            //var data = my_list.AsQueryable();

            //mock_boards.As<IQueryable<Board>>().Setup(m => m.Provider).Returns(data.Provider);
            //mock_boards.As<IQueryable<Board>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //mock_boards.As<IQueryable<Board>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mock_boards.As<IQueryable<Board>>().Setup(m => m.Expression).Returns(data.Expression);

            //mock_context.Setup(m => m.Boards).Returns(mock_boards.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            //mock_context = new Mock<BoardContext>();
            //mock_boards = new Mock<DbSet<Board>>();
            //my_list = new List<Board>();
            //owner = new ApplicationUser();
            //user1 = new ApplicationUser();
            //user2 = new ApplicationUser();

        }

        [TestCleanup]
        public void Cleanup()
        {
            //mock_context = null;
            //mock_boards = null;
            //my_list = null;
        }

        [TestMethod]
        public void DataRepoEnsureCanCreateInstance()
        {

        }
    }
}
