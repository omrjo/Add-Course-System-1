using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CourseMvc.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int CourseCredit { get; set; }

        public static SelectList GetCourses()
        {
            List<SelectListItem> list1 = new List<SelectListItem>();
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
                @"Integrated Security=True;Connect Timeout=30");
            var query = "select CourseID, CourseName from Courses";
            var cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list1.Add(new SelectListItem { Text = reader["CourseName"].ToString(), Value = reader["CourseID"].ToString() });
            }
            con.Close();
            return new SelectList(list1, "Value", "Text");
        }
        public int getUserID(string UserName)
        {
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
               @"Integrated Security=True;Connect Timeout=30");
            var cmd = new SqlCommand("select UserID from Users where UserName = @user", con);
            cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar)).Value = UserName;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int UserID = reader.GetInt32(0);
            reader.Close();
            cmd.Dispose();
            con.Dispose();
            return UserID;
            
            
        }
        public void addCourse(int UserID,int? CourseID)
        {
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
             @"Integrated Security=True;Connect Timeout=30");
            con.Open();
            var cmd1 = new SqlCommand("insert into UsersCourses values (@user, @course)", con);
            cmd1.Parameters.Add(new SqlParameter("@user", SqlDbType.Int)).Value = UserID;
            cmd1.Parameters.Add(new SqlParameter("@course", SqlDbType.Int)).Value = CourseID;

            cmd1.ExecuteNonQuery();

            cmd1.Dispose();
            con.Dispose();

        }
        public List<Course> displayCourse(int UserID)
        {
            List<Course> CourseList = new List<Course>();
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
               @"Integrated Security=True;Connect Timeout=30");
            con.Open();
            var cmd2 = new SqlCommand("select Courses.CourseID, Courses.CourseName, Courses.CourseCredit from Courses inner join UsersCourses on " +
                "[Courses].[CourseID] = [UsersCourses].[CourseID] where UsersCourses.UserID = @user", con);
            cmd2.Parameters.Add(new SqlParameter("@user", SqlDbType.Int)).Value = UserID;
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                {
                    CourseList.Add(new Course()
                    {
                        CourseID = reader.GetInt32(0),
                        CourseName = reader.GetString(1),
                        CourseCredit = reader.GetInt32(2)
                    });
                }
            }
                reader.Close();
            cmd2.Dispose();
            con.Dispose();
            return CourseList;
        }

        public void deleteCourse(int CourseID)
        {
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
              @"Integrated Security=True;Connect Timeout=30");
            con.Open();
            var cmd = new SqlCommand("delete from UsersCourses where CourseID = @course", con);
            cmd.Parameters.Add(new SqlParameter("@course", SqlDbType.Int)).Value = CourseID;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Dispose();
        }
    }
}