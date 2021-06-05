using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string name)
        {
            HttpCookie cookie = new HttpCookie("user", "teste");
            cookie.Expires = DateTime.Now.AddDays(40);
            Response.Cookies.Add(cookie);
            ViewBag.Message = cookie.Value;
            return View();
        }

        public ActionResult Contact()
        {
            var cookie = Request.Cookies["user"];
            ViewBag.Message = cookie.Value;

            return View();
        }
    }
}