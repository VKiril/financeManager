using FinanceManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sendMail = MailerService.SendEmail("everest0corp@gmail.com", "subject", "hello world message");
            ViewBag.Title = "Home Page";

            var user = Request.LogonUserIdentity.Name;
            var user2 = System.Web.HttpContext.Current.User.Identity.Name;

            return View();
        }
    }
}
