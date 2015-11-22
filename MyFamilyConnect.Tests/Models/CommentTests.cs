using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFamilyConnect.Models;

namespace MyFamilyConnect.Tests.Models
{
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
            DateTime time = new DateTime();
            Comment comment = new Comment()
            {
                CommentId = 1, Text = "this is my comment", Owner = user1, TimeStamp = time
            };
            Assert.AreEqual(1, comment.CommentId);
            Assert.AreEqual("this is my comment", comment.Text);
            Assert.AreEqual(user1, comment.Owner);
            Assert.AreEqual(time, comment.TimeStamp);
        }
    }
}
