using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFamilyConnect.Models;

namespace MyFamilyConnect.Tests.Models
{
    // Since this class is a data model, we only need to test here that an instance can be created and that the properties can be correctly assigned and retrieved.
    [TestClass]
    public class UserProfileTests
    {
        [TestMethod]
        public void UserProfileEnsureCanCreateInstance()
        {
            UserProfile profile = new UserProfile();
            Assert.IsNotNull(profile);
        }

        [TestMethod]
        public void UserProfileEnsurePropertiesWork()
        {
            // Arrange
            DateTime birthdate = new DateTime();
            ApplicationUser user1 = new ApplicationUser();

            // Act
            UserProfile profile = new UserProfile()
            {
                AboutMe = "about me", Address1 = "address1", Address2 = "address2", Birthdate = birthdate, City = "city", Email = "email", FirstName = "firstname", LastName = "lastname", Phone1 = "99912345", Phone2 = "99991234", State = "TN", UserProfileId = 1, Zip = "37087"
            };

            // Assert
            Assert.AreEqual("about me", profile.AboutMe);
            Assert.AreEqual("address1", profile.Address1);
            Assert.AreEqual("address2", profile.Address2);
            Assert.AreEqual(birthdate, profile.Birthdate);
            Assert.AreEqual("city", profile.City);
            Assert.AreEqual("email", profile.Email);
            Assert.AreEqual("firstname", profile.FirstName);
            Assert.AreEqual("lastname", profile.LastName);
            //Assert.AreEqual(user1, profile.Owner);
            Assert.AreEqual("99912345", profile.Phone1);
            Assert.AreEqual("99991234", profile.Phone2);
            Assert.AreEqual("TN", profile.State);
            Assert.AreEqual("37087", profile.Zip);
            Assert.AreEqual(1, profile.UserProfileId);
        }
    }
}
