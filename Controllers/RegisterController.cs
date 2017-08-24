using CourseMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseMvc.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName,string UserPassword,DateTime? Date)
        {
            var user = new User();
            user.addUser(UserName, UserPassword, Date);
            return RedirectToAction("Reg");
        }
    }
}