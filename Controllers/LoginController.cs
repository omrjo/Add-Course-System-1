using CourseMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.UI;

namespace CourseMvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index(string UserName)
        {
            var course = new Course();
            int UserID = course.getUserID(UserName);
            ViewBag.CourseList = course.displayCourse(UserID);
            ViewBag.CourseID = Course.GetCourses();
            var e = TempData["error"];
            if (e != null)
            {
                ViewData["error"] = e;
            }
            return View();
        }
        [HttpPost]
        public ActionResult addCourse(int? CourseID,string UserName)
        {
            var course = new Course();
            int UserID = course.getUserID(UserName);
            if (CourseID != null)
            {
                try
                {
                    course.addCourse(UserID, CourseID);
                    return RedirectToAction("Index", "Login", new { UserName = UserName });
                }
                catch (Exception)
                {
                    TempData["error"] = "This course is already added, select another one.";
                }
            }
            else
            {
                TempData["error"] = "Please select course.";
            }
            return RedirectToAction("Index", "Login", new { UserName = UserName });
        }

        public ActionResult deleteCourse(int CourseID, string UserName)
        {
            var course = new Course();
            course.deleteCourse(CourseID);
            return RedirectToAction("Index","Login",new { UserName = UserName});
        }
    }
}