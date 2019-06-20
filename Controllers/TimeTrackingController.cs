using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Time_Tracking.Models;
using Time_Tracking.Services;

namespace Time_Tracking.Controllers
{
    [Route("v1")]
    [ApiController]
    
    public class TimeTrackingController : Controller
    {
        

        private readonly IEmployeeDataServices _services;
        public TimeTrackingController(IEmployeeDataServices services)
        {
            _services = services;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Index")]
        public ActionResult Index()
        {
            var emp_data = _services.GetEmployeeData();

            if (emp_data.Count == 0)
            {
                return NotFound();
            }

            return View(emp_data.ToList());
        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("AddEmployeeData")]
        public ActionResult<Employee> AddEmployeeData(Employee emp)
        {
            var emp_data = _services.AddEmployeeData(emp);

            if (emp_data == null)
            {
                return NotFound();
            }

            return emp_data;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetEmployeeData")]
        public ActionResult<List<Employee>> GetEmployeeData()
        {
            var emp_data = _services.GetEmployeeData();

            if (emp_data.Count == 0)
            {
                return NotFound();
            }

            return emp_data;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult Create(Employee emp)
        {
            if (!ModelState.IsValid)
                return View();

            var emp_data = _services.AddEmployeeData(emp);

            if (emp_data == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}