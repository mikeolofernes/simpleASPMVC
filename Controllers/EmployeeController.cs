using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Time_Tracking.Models;
using Time_Tracking.Services;

namespace Time_Tracking.Controllers
{

    public class EmployeeController : Controller
    {
        private readonly IEmployeeDataServices _services;
        public EmployeeController(IEmployeeDataServices services)
        {
            _services = services;
        }
        // GET: Employee
        public ActionResult Index(string searchBy, string search = "", Boolean? isActive = null)
        {
            var emp_data = _services.GetEmployeeData();

            if (emp_data.Count == 0)
            {
                return View();
            }

            if(searchBy == "Name" && search != null)
            {
                emp_data = emp_data.Where(x => x.Active == isActive).ToList();
                return View(emp_data.Where(x => x.EmployeeName.ToLower().Contains(search.ToLower())).ToList());
            }
            else if (searchBy == "UserID" && search != null)
            {
                emp_data = emp_data.Where(x => x.Active == isActive).ToList();
                return View(emp_data.Where(x => x.UserID == Convert.ToInt32(search)).ToList());
            }
            else if (isActive != null)
            {
                emp_data = emp_data.Where(x => x.Active == isActive).ToList();
            }
            return View(emp_data.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var emp_data = _services.GetEmployeeDataByID(id);

            if (emp_data == null)
            {
                return NotFound();
            }

            return View(emp_data);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {
            try
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
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var emp_data = _services.GetEmployeeDataByID(id);

            if (emp_data == null)
            {
                return NotFound();
            }

            return View(emp_data);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                var update = _services.EditEmployeeData(emp);
                if (update == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {

                var emp_data = _services.GetEmployeeDataByID(id);

                if (emp_data == null)
                {
                    return NotFound();
                }

                return View(emp_data);
            }
            catch
            {
                return View();
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {

                var delete = _services.DeleteEmployeeData(id);
                if (delete == 0)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}