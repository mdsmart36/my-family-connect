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
                throw;
            }                        
        }

        public List<NewsPhotoItem> GetNewsPhotosForUser(int userProfileId)
        {
            try
            {
                var query = from l in context.NewsAndPhotos where l.UserProfileId == userProfileId select l;
                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool UpdateNewsPhotoTitle(int userProfileId, string oldTitle, string newTitle)
        {
            var success = true;
            try
            {
                var query = context.NewsAndPhotos.Where(n => n.UserProfileId == userProfileId).Where(n => n.Title == oldTitle);
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

        public NewsPhotoItem DeleteNewsPhotoItem(NewsPhotoItem news_item1)
        {
            var result = new NewsPhotoItem();
            try
            {
                var query = context.NewsAndPhotos.Where(n => n == news_item1);
                result = query.First();
                context.NewsAndPhotos.Remove(result);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }            
            return result;
        }
    }
}
