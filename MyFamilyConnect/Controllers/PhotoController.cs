using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFamilyConnect.Models;
using System.Threading.Tasks;

namespace MyFamilyConnect.Controllers
{
    public class PhotoController : Controller
    {
        private DataRepository repository;

        public PhotoController()
        {
            repository = new DataRepository();
        }

        public PhotoController(DataRepository data_repo)
        {
            repository = data_repo;
        }

        // GET: Photo
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "My Photos";
            int profile_id = repository.GetCurrentUserProfile().UserProfileId;
            List<Photo> my_photo_items = repository.GetPhotosForUser(profile_id);
            return View(my_photo_items);            
        }

        [Authorize]
        public ActionResult AllPhotos()
        {
            ViewBag.Title = "All Photos";
            ViewBag.CurrentUserId = repository.GetCurrentUserProfile().UserProfileId;
            List<Photo> all_photos = repository.GetAllPhotoItems();
            return View("Index", all_photos);
        }


        // GET: Photo/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Photo item_to_show = repository.GetPhotoItem(id);
            ViewBag.CurrentPhotoId = item_to_show.PhotoId;
            ViewBag.CurrentUserId = repository.GetCurrentUserProfile().UserProfileId;
            return View(item_to_show);            
        }

        // GET: Photo/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Photo/Create
        [HttpPost, Authorize]
        public ActionResult Create(Photo item_to_add, HttpPostedFileBase upload, FormCollection form)
        {
            try
            {
                UserProfile profile = repository.GetCurrentUserProfile();
                //item_to_add.Title = form.Get("photo-title");
                //item_to_add.Text = form.Get("photo-text");
                item_to_add.UserProfile = profile;
                item_to_add.Comments = null;                

                if (upload != null && upload.ContentLength > 0)
                {                    
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        item_to_add.Content = reader.ReadBytes(upload.ContentLength);
                    }

                }                

                if (ModelState.IsValid)
                {
                    repository.AddPhotoItem(item_to_add);
                    return RedirectToAction("Index");
                }                
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: Photo/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Photo item_to_edit = repository.GetPhotoItem(id);
            return View(item_to_edit);
        }

        // POST: Photo/Edit/5
        [HttpPost, Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Photo item_to_edit = repository.GetPhotoItem(id);
                item_to_edit.TimeStamp = DateTime.Now;
                if (TryUpdateModel(item_to_edit, "", new string[] { "Title", "Text", "Content", "TimeStamp" }))
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

        // GET: Photo/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Photo item_to_delete = repository.GetPhotoItem(id);
            return View(item_to_delete);
        }

        // POST: Photo/Delete/5
        [HttpPost, Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repository.DeletePhotoItem(id);                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost, Authorize]
        public ActionResult CreatePhotoComment(Comment comment_to_add, FormCollection form)
        {
            int comment_photo_id = int.Parse(form.Get("photo-id"));
            comment_to_add.Text = form.Get("comment-text");
            comment_to_add.UserProfile = repository.GetCurrentUserProfile();
            comment_to_add.NewsItem = null;
            comment_to_add.PhotoItem = repository.GetPhotoItem(comment_photo_id);
            repository.AddComment(comment_to_add);
            return RedirectToAction("Details", new { id = comment_photo_id });
        }
    }
}
