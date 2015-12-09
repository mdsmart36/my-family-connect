using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFamilyConnect.Models;

namespace MyFamilyConnect.Controllers
{
    public class HomeController : Controller
    {
        private DataRepository repository;
        public HomeController()
        {
            repository = new DataRepository();
        }

        public HomeController(DataRepository data_repo)
        {
            repository = data_repo;
        }

        public ActionResult Index()
        {
            // if there is no user profile, create it
            if (Request.IsAuthenticated)
            {
                //string user_id = User.Identity.GetUserId();
                //ApplicationUser me = repository.Users.FirstOrDefault(u => u.Id == user_id);
                ApplicationUser me = repository.GetCurrentApplicationUser();
                if (repository.GetUserProfile(me) == null)
                {
                    repository.AddUserProfile(new UserProfile { Owner = me });
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}