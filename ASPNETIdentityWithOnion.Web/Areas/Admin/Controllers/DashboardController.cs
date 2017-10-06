using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETIdentityWithOnion.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Notifications()
        {
            return PartialView();
        }

        public PartialViewResult TeamMembers()
        {
            return PartialView();
        }

        public PartialViewResult Calendar()
        {
            return PartialView();
        }
    }
    
}