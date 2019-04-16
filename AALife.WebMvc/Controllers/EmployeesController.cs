using AALife.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetEmployees(int? EmployeeId)
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee(1, "Arun", true, null));
            employees.Add(new Employee(2, "Pradeep", false, 1));
            employees.Add(new Employee(3, "Gowtham", true, null));
            employees.Add(new Employee(4, "Raj", false, 3));
            if (EmployeeId != null)
            {
                return Json(employees.Where(e => e.ReportsTo == EmployeeId).ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(employees.Where(e => !e.ReportsTo.HasValue).ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}