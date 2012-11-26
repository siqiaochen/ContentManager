using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentManagerMVC.Filters;

namespace ContentManagerMVC.Controllers
{

    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public AccountController AccountController
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
