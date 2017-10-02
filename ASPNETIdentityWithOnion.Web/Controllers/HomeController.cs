using ASPNETIdentityWithOnion.Core.Entities;
using ASPNETIdentityWithOnion.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Controllers {
    public class HomeController : Controller {
        private IProductManager manager;
        public HomeController(IProductManager manager)
        {
            this.manager = manager;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
        public ActionResult Default()
        {
            return View();
        }
        public PartialViewResult Items()
        {

            return PartialView();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && manager != null)
        //    {
        //        if (manager != null)
        //        {
        //            manager.Dispose();
        //            manager = null;
        //        }
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
