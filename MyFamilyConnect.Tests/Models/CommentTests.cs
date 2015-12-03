using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFamilyConnect.Models;

namespace MyFamilyConnect.Tests.Models
{

    // Since this class is a data model, we only need to test here that an instance can be created and that the properties can be correctly assigned and retrieved.
    [TestClass]
    public class CommentTests
    {
        private ApplicationUser user1 = new ApplicationUser();

        [TestMethod]
        public void CommentEnsureCanCreateInstance()
        {
            Comment comment = new Comment();
            Assert.IsNotNull(comment);
        }

        [TestMethod]
        public void CommentEnsurePropertiesWork()
        {
            // Arrange
            DateTime time = new DateTime();
            // Act
            Comment comment = new Comment()
            {
                CommentId = 1, Text = "this is my comment", TimeStamp = time
            };
            // Assert
            Assert.AreEqual(1, comment.CommentId);
            Assert.AreEqual("this is my comment", comment.Text);
            //Assert.AreEqual(1, comment.UserProfileId);
            Assert.AreEqual(time, comment.TimeStamp);
        }
    }
}
