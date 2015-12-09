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
    public class UserProfilesController : Controller
    {
        private DataRepository repository;

        public UserProfilesController()
        {
            repository = new DataRepository();
        }

        public UserProfilesController(DataRepository data_repo)
        {
            repository = data_repo;
        }

        // GET: UserProfiles
        public ActionResult Index()
        {
            // Get the user profile
            List<UserProfile> profiles = repository.GetAllUserProfiles();
            return View(profiles);
        }

        // GET: UserProfiles/Details/5
        //public ActionResult Details(int id)
        public ActionResult Details()
        {
            //UserProfile item_to_show = repository.GetUserProfile(id);
            UserProfile item_to_show = repository.GetCurrentUserProfile();
            return View(item_to_show);            
        }

        // GET: UserProfiles/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: UserProfiles/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserProfiles/Edit/5
        [Authorize]
        public ActionResult Edit()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser me = repository.Users.FirstOrDefault(u => u.Id == user_id);
            UserProfile item_to_edit = repository.GetUserProfile(me);
            return View(item_to_edit);            
        }

        // POST: UserProfiles/Edit/5
        [HttpPost, Authorize]
        //public ActionResult Edit(int id, FormCollection collection)
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                string user_id = User.Identity.GetUserId();
                ApplicationUser me = repository.Users.FirstOrDefault(u => u.Id == user_id);
                UserProfile item_to_edit = repository.GetUserProfile(me);
                if (TryUpdateModel(item_to_edit, "", new string[] { "FirstName", "LastName", "Birthday", "Address1", "Address2", "City", "State", "Zip", "Phone1", "Phone2", "Email", "AboutMe" }))
                {
                    repository.SaveAllChanges();
                };
                                
                return RedirectToAction("Index","Home");
                
            }
            catch
            {
                return View();
            }
            
        }

        // GET: UserProfiles/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: UserProfiles/Delete/5
        //[HttpPost, Authorize]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        repository.DeleteUserProfile(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }

        //}
    }
}
