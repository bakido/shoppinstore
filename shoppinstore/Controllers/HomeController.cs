using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace shoppinstore.Controllers
{
    public class HomeController : Controller
    {
        ApplicationUserManager _userManager;

        public HomeController()
        {

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            if (this.User.Identity.GetUserId() == null)
            {        
                return RedirectToAction("showProducts", "Cart");
            }
            var LoggedInUserRole = this.UserManager.GetRoles(this.User.Identity.GetUserId());
            if(LoggedInUserRole.Contains("Admin"))
            {
                return RedirectToAction("AddNewProductToTheStore", "Product");
            }
            return RedirectToAction("showProducts", "Cart");
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