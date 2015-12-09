using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFamilyConnect.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace MyFamilyConnect.Controllers
{
    public class NewsPhotoItemController : Controller
    {
        private DataRepository repository;        

        public NewsPhotoItemController()
        {
            repository = new DataRepository();
        }

        public NewsPhotoItemController(DataRepository data_repo)
        {
            repository = data_repo;
        }

        // GET: NewsPhotoItem
        [Authorize]
        public ActionResult Index()
        {
            int profile_id = repository.GetCurrentUserProfile().UserProfileId;
            List<NewsPhotoItem> my_news_items = repository.GetNewsPhotosForUser(profile_id);
            //List<NewsPhotoItem> news_items = repository.GetAllNewsPhotoItems();
            ViewBag.Title = "My News and Photos";
            return View(my_news_items);
        }

        // GET: NewsPhotoItem/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            NewsPhotoItem item_to_show = repository.GetNewsPhotoItem(id);
            return View(item_to_show);
        }

        // GET: NewsPhotoItem/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsPhotoItem/Create
        [HttpPost, Authorize]
        public ActionResult Create(FormCollection form)
        {

            try
            {
                //string user_id = User.Identity.GetUserId();
                //ApplicationUser me = repository.Users.FirstOrDefault(u => u.Id == user_id);
                //UserProfile profile = repository.GetUserProfile(me);
                UserProfile profile = repository.GetCurrentUserProfile();
                string news_title = form.Get("news-title");
                string news_text = form.Get("news-text");
                bool news_has_photo = Convert.ToBoolean(form.Get("news-has-photo").Split(',')[0]);
                byte[] news_image = null;
                NewsPhotoItem item_to_add = new NewsPhotoItem
                {
                    Title = news_title,
                    Text = news_text,
                    HasPhoto = news_has_photo,
                    Photo = news_image,
                    UserProfile = profile,
                    Comments = null
                };

                repository.AddNewsPhotoItem(item_to_add);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsPhotoItem/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            NewsPhotoItem item_to_edit = repository.GetNewsPhotoItem(id);
            return View(item_to_edit);            
        }

        // POST: NewsPhotoItem/Edit/5
        [HttpPost, Authorize]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                NewsPhotoItem item_to_edit = repository.GetNewsPhotoItem(id);
                item_to_edit.TimeStamp = DateTime.Now;
                if (TryUpdateModel(item_to_edit, "", new string[] { "Title", "Text", "HasPhoto", "TimeStamp" }))
                {
                    repository.SaveAllChanges();
                };
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsPhotoItem/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            NewsPhotoItem item_to_delete = repository.GetNewsPhotoItem(id);
            return View(item_to_delete);                                            
        }

        // POST: NewsPhotoItem/Delete/5
        [HttpPost, Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {                                                
                repository.DeleteNewsPhotoItem(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
