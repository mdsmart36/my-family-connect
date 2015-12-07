using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyFamilyConnect.Models;

namespace MyFamilyConnect.Controllers
{
    public class NewsPhotoItemsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: NewsPhotoItems
        public ActionResult Index()
        {
            return View(db.NewsAndPhotos.ToList());
        }

        // GET: NewsPhotoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPhotoItem newsPhotoItem = db.NewsAndPhotos.Find(id);
            if (newsPhotoItem == null)
            {
                return HttpNotFound();
            }
            return View(newsPhotoItem);
        }

        // GET: NewsPhotoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsPhotoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsPhotoItemId,Title,Text,HasPhoto,Photo,TimeStamp")] NewsPhotoItem newsPhotoItem)
        {
            if (ModelState.IsValid)
            {
                db.NewsAndPhotos.Add(newsPhotoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsPhotoItem);
        }

        // GET: NewsPhotoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPhotoItem newsPhotoItem = db.NewsAndPhotos.Find(id);
            if (newsPhotoItem == null)
            {
                return HttpNotFound();
            }
            return View(newsPhotoItem);
        }

        // POST: NewsPhotoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsPhotoItemId,Title,Text,HasPhoto,Photo,TimeStamp")] NewsPhotoItem newsPhotoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsPhotoItem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsPhotoItem);
        }

        // GET: NewsPhotoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPhotoItem newsPhotoItem = db.NewsAndPhotos.Find(id);
            if (newsPhotoItem == null)
            {
                return HttpNotFound();
            }
            return View(newsPhotoItem);
        }

        // POST: NewsPhotoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsPhotoItem newsPhotoItem = db.NewsAndPhotos.Find(id);
            db.NewsAndPhotos.Remove(newsPhotoItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
