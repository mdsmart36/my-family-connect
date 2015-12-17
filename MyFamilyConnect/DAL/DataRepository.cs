using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace MyFamilyConnect.Models
{
    public class DataRepository
    {
        private DataContext context;
        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public DataRepository()
        {
            context = new DataContext();
        }

        public DataRepository(DataContext _context)
        {
            context = _context;
        }

        /****************************************
        *
        * CRUD OPERATIONS FOR NEWSPHOTO RECORDS
        *
        *****************************************/

        public int GetAllNewsCount()
        {            
            return GetAllNewsItems().Count;
        }

        public bool AddNewsItem(News news_item)
        {
            bool result = true;
            try
            {
                news_item.TimeStamp = DateTime.Now;
                context.News.Add(news_item);
                context.SaveChanges();                
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public List<News> GetAllNewsItems()
        {
            try
            {
                var query = from l in context.News select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
                //throw;
            }                        
        }

        public List<News> GetNewsForUser(int userProfileId)
        {
            try
            {
                var query = from l in context.News where l.UserProfile.UserProfileId == userProfileId select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public bool UpdateNewsTitle(int userProfileId, string oldTitle, string newTitle)
        {
            var success = true;
            try
            {
                var query = context.News.Where(n => n.UserProfile.UserProfileId == userProfileId).Where(n => n.Title == oldTitle);
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

        public bool UpdateNewsItem(News item_to_update)
        {
            context.Entry(item_to_update).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public bool SaveAllChanges()
        {
            context.SaveChanges();
            return true;
        }

        public bool DeleteNewsItem(int newsId)
        {
            var result = true;
            try
            {
                // THIS IS THE STANDARD WAY TO SEARCH AND DELETE WHEN THERE AREN'T CHILD ENTITIES
                //var query = context.News.Where(n => n.NewsId == newsPhotoId);
                //var itemToDelete = query.First();
                //context.News.Remove(itemToDelete);

                // THIS IS A WAY TO DO 'EAGER LOADING' (LOADING THE CHILD ENTITIES INTO THE CONTEXT)
                // SEE http://stackoverflow.com/questions/15226312/entityframewok-how-to-configure-cascade-delete-to-nullify-foreign-keys
                var query = context.Set<News>().Include(m => m.Comments).SingleOrDefault(n => n.NewsId == newsId);
                context.Set<News>().Remove(query);
                context.SaveChanges();
            }
            catch (Exception)
            {
                result = false;               
            }            
            return result;
        }

        public News GetNewsItem(int NewsId)
        {
            News found = new News();
            try
            {
                var query = context.News.Where(n => n.NewsId == NewsId);
                found = query.First();
            }
            catch (Exception)
            {
                found = null;                
            }
            return found;
        }

        public bool UpdateNewsProperty(int NewsId, string propertyName, object propertyValue)
        {
            var isSuccessful = true;
            try
            {
                var query = context.News.Where(a => a.NewsId == NewsId);
                var found = query.First();

                Type n = typeof(News);
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

        public UserProfile GetUserProfile(ApplicationUser owner)
        {
            try
            {
                var query = from p in context.UserProfiles where p.Owner.Id == owner.Id select p;
                return query.First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ApplicationUser GetCurrentApplicationUser()
        {
            string user_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var query = context.Users.Where(u => u.Id == user_id);
            ApplicationUser me = query.FirstOrDefault();
            return me;
        }

        public UserProfile GetCurrentUserProfile()
        {
            var me = GetCurrentApplicationUser();
            return GetUserProfile(me);
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
                comment.TimeStamp = DateTime.Now;
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

        /****************************************
        *
        * CRUD OPERATIONS FOR PHOTO RECORDS
        *
        *****************************************/

        public int GetAllPhotosCount()
        {
            return GetAllPhotoItems().Count;
        }

        public bool AddPhotoItem(Photo photo_item)
        {
            bool result = true;
            try
            {
                photo_item.TimeStamp = DateTime.Now;
                context.Photos.Add(photo_item);
                context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }

        public List<Photo> GetAllPhotoItems()
        {
            try
            {
                var query = from l in context.Photos select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
        }

        public List<Photo> GetPhotosForUser(int userProfileId)
        {
            try
            {
                var query = from l in context.Photos where l.UserProfile.UserProfileId == userProfileId select l;
                return query.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdatePhotoTitle(int userProfileId, string oldTitle, string newTitle)
        {
            var success = true;
            try
            {
                var query = context.Photos.Where(n => n.UserProfile.UserProfileId == userProfileId).Where(n => n.Title == oldTitle);
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

        public bool UpdatePhotoItem(Photo item_to_update)
        {
            context.Entry(item_to_update).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        }

        public bool DeletePhotoItem(int photoId)
        {
            var result = true;
            try
            {
                // THIS IS THE STANDARD WAY TO SEARCH AND DELETE WHEN THERE AREN'T CHILD ENTITIES
                //var query = context.Photos.Where(n => n.PhotoId == photoId);
                //var itemToDelete = query.First();
                //context.Photos.Remove(itemToDelete);

                // THIS IS A WAY TO DO 'EAGER LOADING' (LOADING THE CHILD ENTITIES INTO THE CONTEXT),
                // DELETE THE PARENT, AND NULLIFY THE FOREIGN KEYS IN THE CHILD
                // SEE http://stackoverflow.com/questions/15226312/entityframewok-how-to-configure-cascade-delete-to-nullify-foreign-keys
                var query = context.Set<Photo>().Include(m => m.Comments).SingleOrDefault(n => n.PhotoId == photoId);
                context.Set<Photo>().Remove(query);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                result = false;
                throw;
            }
            return result;
        }

        public Photo GetPhotoItem(int photoId)
        {
            Photo found = new Photo();
            try
            {
                var query = context.Photos.Where(n => n.PhotoId == photoId);
                found = query.First();
            }
            catch (Exception)
            {
                found = null;
            }
            return found;
        }

        public bool UpdatePhotoProperty(int photoId, string propertyName, object propertyValue)
        {
            var isSuccessful = true;
            try
            {
                var query = context.Photos.Where(a => a.PhotoId == photoId);
                var found = query.First();

                Type n = typeof(Photo);
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
    }
}
