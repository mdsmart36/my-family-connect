using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFamilyConnect.Models;
using System.Collections.Generic;

namespace MyFamilyConnect.Tests.Models
{
    // Since this class is a data model, we only need to test here that an instance can be created and that the properties can be correctly assigned and retrieved.
    [TestClass]
    public class NewsPhotoTests
    {
        [TestMethod]
        public void NewsPhotoEnsureCanCreateInstance()
        {
            NewsPhotoItem news = new NewsPhotoItem();
            Assert.IsNotNull(news);
        }

        [TestMethod]
        public void NewsPhotoEnsurePropertiesWork()
        {
            // Arrange
            DateTime timestamp = new DateTime();
            ApplicationUser user1 = new ApplicationUser();
            Comment comment = new Comment() { Text = "comment" };
            List<Comment> comments = new List<Comment>();
            comments.Add(comment);
            // Act
            NewsPhotoItem news = new NewsPhotoItem()
            { Title = "title", HasPhoto = false, Comments = comments, NewsPhotoId = 2, UserProfileId = 1, Photo = null, Text = "news text", TimeStamp = timestamp
            };
            // Assert
            Assert.AreEqual("title", news.Title);
            Assert.AreEqual(false, news.HasPhoto);
            Assert.AreEqual(comments, news.Comments);
            Assert.AreEqual(2, news.NewsPhotoId);
            Assert.AreEqual(1, news.UserProfileId);
            Assert.AreEqual(null, news.Photo);
            Assert.AreEqual("news text", news.Text);
            Assert.AreEqual(timestamp, news.TimeStamp);
        }
    }
}
