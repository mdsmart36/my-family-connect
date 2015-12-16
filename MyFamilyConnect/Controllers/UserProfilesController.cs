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
        [Authorize]
        public ActionResult Index()
        {            
            List<UserProfile> profiles = repository.GetAllUserProfiles();
            return View(profiles);
        }
        
        [Authorize]
        public ActionResult Details()
        {
            UserProfile item_to_show = repository.GetCurrentUserProfile();
            return View(item_to_show);            
        }        

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
    }
}
