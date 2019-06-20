using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracking.Models;

namespace Time_Tracking.Services
{
    public class EmployeeDataServices : IEmployeeDataServices
    {

        private readonly Dictionary<Int32, Employee> _employee;
        private Int32 _UserId = 5;
        public EmployeeDataServices()
        {
            _employee = new Dictionary<Int32, Employee>();
            Employee newEmp = new Employee();

            for (int i = 1; i < 5; i++)
            {
                newEmp.UserID = i;
                newEmp.EmployeeName = "Michael Olofernes " + i;
                newEmp.ClockInTime = Convert.ToDateTime("06/06/2019 6:52").AddDays(i);
                newEmp.ClockOutTime = Convert.ToDateTime("06/06/2019 4:19").AddDays(i);
                newEmp.Active = true;

                _employee.Add(i, newEmp);
            }   
            
        }
        public Employee AddEmployeeData(Employee emp)
        {
            emp.UserID = _UserId += 1;
            _employee.Add(emp.UserID, emp);

            return emp;
        }

        public List<Employee> GetEmployeeData()
        {
           
            var data = (from emp in _employee
                        orderby emp.Value.UserID
                        select new Employee()
                        {
                            UserID = emp.Value.UserID,
                            EmployeeName = emp.Value.EmployeeName,
                            ClockInTime = emp.Value.ClockInTime,
                            ClockOutTime = emp.Value.ClockOutTime,
                            Active = emp.Value.Active,

                        }
                        ).ToList();
            
            return data;
        }

        public Employee GetEmployeeDataByID(Int32 id)
        {
            var data = (from emp in _employee
                        where emp.Value.UserID == id
                        select new Employee()
                        {
                            UserID = emp.Value.UserID,
                            EmployeeName = emp.Value.EmployeeName,
                            ClockInTime = emp.Value.ClockInTime,
                            ClockOutTime = emp.Value.ClockOutTime,
                            Active = emp.Value.Active,

                        }
                        ).FirstOrDefault();
            return data;
        }

        public Employee EditEmployeeData(Employee emp)
        {
            _employee[emp.UserID].EmployeeName = emp.EmployeeName;
            _employee[emp.UserID].ClockInTime = emp.ClockInTime;
            _employee[emp.UserID].ClockOutTime = emp.ClockOutTime;
            _employee[emp.UserID].Active = emp.Active;            

            return emp;
        }

        public Int32 DeleteEmployeeData(Int32 id)
        {
            if(id != 0)
            {
                _employee.Remove(id);
                return 1;
            }
            else
            {
                return 0;
            }    
   
        }
    }
}
