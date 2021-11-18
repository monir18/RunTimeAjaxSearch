using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RunTimeAjaxSearch.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RunTimeAjaxSearch.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection("Server=MONIR\\SQL19;Database=test_mvc;User Id=sa;Password=flora@73210m");
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        public IActionResult Index()
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from tblCustomer";
            dr = com.ExecuteReader();

            List<customer> customers = new List<customer>();

            while(dr.Read())
            {
                var cus = new customer()
                {
                    //id = dr.GetInt32(0),
                    id = dr.GetInt32(0),
                    firstname = dr.GetString(1),
                    lastname=dr.GetString(2),
                    email=dr.GetString(3)
                };

                customers.Add(cus);
            }

            con.Close();

            ViewBag.cuslist = customers;
            
            return View();
        }

        
        public JsonResult SearchRecord(string searchoption,string searchtext)
        {
            con.Open();
            com.Connection = con;
            if(searchoption=="id")
            {
                com.CommandText = "Select * from tblCustomer where id like '%"+searchtext+"%'";
            }
            else if (searchoption == "firstname")
            {
                com.CommandText = "Select * from tblCustomer where firstname like '%" + searchtext + "%'";
            }
            else if (searchoption == "lastname")
            {
                com.CommandText = "Select * from tblCustomer where lastname like '%" + searchtext + "%'";
            }
            else if (searchoption == "email")
            {
                com.CommandText = "Select * from tblCustomer where email like '%" + searchtext + "%'";
            }

            dr = com.ExecuteReader();

            List<customer> customers = new List<customer>();

            while (dr.Read())
            {
                var cus = new customer()
                {
                    id = dr.GetInt32(0),
                    firstname = dr.GetString(1),
                    lastname = dr.GetString(2),
                    email = dr.GetString(3)
                };

                customers.Add(cus);
            }

            con.Close();

            return Json(customers);
        }


    }
}
