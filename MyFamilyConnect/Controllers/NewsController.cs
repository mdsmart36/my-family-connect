﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFamilyConnect.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.IO;
using System.Drawing;

namespace MyFamilyConnect.Controllers
{
    public class NewsController : Controller
    {
        private DataRepository repository;        

        public NewsController()
        {
            repository = new DataRepository();
        }

        public NewsController(DataRepository data_repo)
        {
            repository = data_repo;
        }

        // GET: NewsPhotoItem
        [Authorize]
        public ActionResult Index()
        {
            // Get all the news and photos associated with the current user
            ViewBag.Title = "My News";
            int profile_id = repository.GetCurrentUserProfile().UserProfileId;
            List<News> my_news_items = repository.GetNewsForUser(profile_id);                        
            return View(my_news_items);
        }

        // GET: NewsPhotoItem/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {            
            News item_to_show = repository.GetNewsItem(id);
            //MemoryStream ms = new MemoryStream(item_to_show.Photo);
            //Image returnImage = Image.FromStream(ms);
            //ViewBag.Photo = returnImage;
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
        //public ActionResult Create(FormCollection form)
        public ActionResult Create(News item_to_add, FormCollection form)
        {

            try
            {
                UserProfile profile = repository.GetCurrentUserProfile();
                item_to_add.Title = form.Get("news-title");
                item_to_add.Text = form.Get("news-text");
                // include in signature parameters -- HttpPostedFileBase upload
                //bool news_has_photo = Convert.ToBoolean(form.Get("news-has-photo").Split(',')[0]);

                //item_to_add.HasPhoto = false;
                item_to_add.UserProfile = profile;
                item_to_add.Comments = null;
                //byte[] news_image = null;

                //if (upload != null && upload.ContentLength > 0)
                //{
                    //var photo = new File
                    //{
                    //    FileName = System.IO.Path.GetFileName(upload.FileName),
                    //    FileType = FileType.Avatar,
                    //    ContentType = upload.ContentType
                    //};
                    //item_to_add.HasPhoto = true;
                //    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                //    {
                //        item_to_add.Photo = reader.ReadBytes(upload.ContentLength);
                //    }
                    
                //}

                //item_to_add
                //{
                //    Title = news_title,
                //    Text = news_text,
                //    HasPhoto = news_has_photo,
                //    Photo = news_image,
                //    UserProfile = profile,
                //    Comments = null
                //};

                repository.AddNewsItem(item_to_add);
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
            News item_to_edit = repository.GetNewsItem(id);
            return View(item_to_edit);            
        }

        // POST: NewsPhotoItem/Edit/5
        [HttpPost, Authorize]
        public ActionResult Edit(int id, FormCollection form)
        {
            try
            {
                News item_to_edit = repository.GetNewsItem(id);
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
            News item_to_delete = repository.GetNewsItem(id);
            return View(item_to_delete);                                            
        }

        // POST: NewsPhotoItem/Delete/5
        [HttpPost, Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {                                                
                repository.DeleteNewsItem(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}