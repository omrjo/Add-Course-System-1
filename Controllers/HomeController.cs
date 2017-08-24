using CourseMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult log()
        {
            var e = ViewBag.Message;
            if (e != null)
            {
                ViewBag.Message = e;
            }
            return View();
        }
        [HttpPost]
        public ActionResult log(string UserName,string UserPassword)
        {
                if (Checklogin(UserName, UserPassword))
                {
                    return RedirectToAction("Index", "Login", new { UserName = UserName });
                }
                else
                {
                     ViewBag.Message = "Incorrect username/password.";
                }

            return View();
        }

        public Boolean Checklogin(string username, string password)
        {
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
                @"Integrated Security=True;Connect Timeout=30");

            string query = @"select [UserName] from [dbo].[Users] where [UserName]=@user and [UserPassword]=@pass";
            var cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = username;
            cmd.Parameters.Add(new SqlParameter("@pass", SqlDbType.NVarChar)).Value = password;
            con.Open();
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else
                return false;
        }
    }
}