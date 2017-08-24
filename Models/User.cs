using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CourseMvc.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public void addUser(string UserName,string UserPassword,DateTime? Date)
        {
            var con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\CourseDB.mdf;" +
            @"Integrated Security=True;Connect Timeout=30");
            if (Date != null)
            {
                var query = "insert into Users values(@username,@pass,@date)";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = UserName;
                cmd.Parameters.Add(new SqlParameter("@pass", SqlDbType.NVarChar)).Value = UserPassword;
                cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.Date)).Value = Date;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            else
            {
                var query = "insert into Users values(@username,@pass,null)";
                var cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar)).Value = UserName;
                cmd.Parameters.Add(new SqlParameter("@pass", SqlDbType.NVarChar)).Value = UserPassword;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
       
    }
}