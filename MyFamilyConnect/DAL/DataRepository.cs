using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MyFamilyConnect.Models
{
    public class DataRepository
    {
        private DataContext context;
        public DataRepository(DataContext _context)
        {
            context = _context;
        }

        /****************************************
        *
        * CRUD OPERATIONS FOR NEWSPHOTO RECORDS
        *
        *****************************************/

        public int GetAllNewsPhotosCount()
        {            
            return GetAllNewsPhotoItems().Count;
        }

        public bool AddNewsPhotoItem(NewsPhotoItem news_item)
        {
            bool result = true;
            try
            {
                context.NewsAndPhotos.Add(news_item);
                context.SaveChanges();                
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public List<NewsPhotoItem> GetAllNewsPhotoItems()
        {
            try
            {
                var query = from l in context.NewsAndPhotos select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
                //throw;
            }                        
        }

        public List<NewsPhotoItem> GetNewsPhotosForUser(int userProfileId)
        {
            try
            {
                var query = from l in context.NewsAndPhotos where l.UserProfile.UserProfileId == userProfileId select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public bool UpdateNewsPhotoTitle(int userProfileId, string oldTitle, string newTitle)
        {
            var success = true;
            try
            {
                var query = context.NewsAndPhotos.Where(n => n.UserProfile.UserProfileId == userProfileId).Where(n => n.Title == oldTitle);
                var result = query.First();
                result.Title = newTitle;
                context.SaveChanges();
            }
            catch (Exception)
            {
                success = false;                
            }
            return success;
            
        }

        public bool DeleteNewsPhotoItem(NewsPhotoItem news_item1)
        {
            var result = true;
            try
            {
                var query = context.NewsAndPhotos.Where(n => n == news_item1);
                var itemToDelete = query.First();
                context.NewsAndPhotos.Remove(itemToDelete);
                context.SaveChanges();
            }
            catch (Exception)
            {
                result = false;               
            }            
            return result;
        }

        public NewsPhotoItem GetNewsPhotoItem(int NewsPhotoItemId)
        {
            NewsPhotoItem found = new NewsPhotoItem();
            try
            {
                var query = context.NewsAndPhotos.Where(n => n.NewsPhotoItemId == NewsPhotoItemId);
                found = query.First();
            }
            catch (Exception)
            {
                found = null;                
            }
            return found;
        }

        public bool UpdateNewsProperty(int NewsPhotoItemId, string propertyName, object propertyValue)
        {
            var isSuccessful = true;
            try
            {
                var query = context.NewsAndPhotos.Where(a => a.NewsPhotoItemId == NewsPhotoItemId);
                var found = query.First();

                Type n = typeof(NewsPhotoItem);
                PropertyInfo propertyToUpdate = n.GetProperty(propertyName);                
                propertyToUpdate.SetValue(found, propertyValue);
                context.SaveChanges();
            }
            catch (Exception)
            {
                isSuccessful = false;
                throw;
            }
            return isSuccessful;
        }

        /****************************************
        *
        * CRUD OPERATIONS FOR USERPROFILE RECORDS
        *
        *****************************************/
        public int GetProfileCount()
        {
            return GetAllUserProfiles().Count;
        }

        public bool AddUserProfile(UserProfile profile)
        {
            bool result = true;
            try
            {
                context.UserProfiles.Add(profile);
                context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            try
            {
                var query = from l in context.UserProfiles select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserProfile GetUserProfile(int profileId)
        {
            try
            {
                var query = from p in context.UserProfiles where p.UserProfileId == profileId select p;
                return query.First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateUserProfile(int UserProfileId, string newName)
        {
            var success = true;
            try
            {
                var query = context.UserProfiles.Where(n => n.UserProfileId == UserProfileId);
                var result = query.First();
                result.FirstName = newName;
                context.SaveChanges();
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool DeleteUserProfile(int UserProfileId)
        {
            var result = true;
            try
            {
                var query = context.UserProfiles.Where(n => n.UserProfileId == UserProfileId);
                var foundProfile = query.First();
                context.UserProfiles.Remove(foundProfile);
                context.SaveChanges();
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        /****************************************
        *
        * CRUD OPERATIONS FOR COMMENT RECORDS
        *
        *****************************************/

        public int GetCommentCount()
        {
            return GetAllComments().Count;
        }

        public bool AddComment(Comment comment)
        {
            bool result = true;
            try
            {
                context.Comments.Add(comment);
                context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public List<Comment> GetAllComments()
        {
            try
            {
                var query = from l in context.Comments select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Comment GetComment(int commentId)
        {
            try
            {
                var query = from l in context.Comments where l.CommentId == commentId select l;
                return query.First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateComment(int commentId, string newText)
        {
            var success = true;
            try
            {
                var query = context.Comments.Where(n => n.CommentId == commentId);
                var result = query.First();
                result.Text = newText;
                context.SaveChanges();
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public bool DeleteComment(int commentId)
        {
            var result = true;
            try
            {
                var query = context.Comments.Where(n => n.CommentId == commentId);
                var foundComment = query.First();
                context.Comments.Remove(foundComment);
                context.SaveChanges();
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

    }
}
