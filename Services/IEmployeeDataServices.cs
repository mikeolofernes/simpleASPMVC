using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracking.Models;

namespace Time_Tracking.Services
{
    public interface IEmployeeDataServices
    {

        Employee AddEmployeeData(Employee emp);
        List<Employee> GetEmployeeData();
        Employee GetEmployeeDataByID(Int32 id);
        Employee EditEmployeeData(Employee emp);
        Int32 DeleteEmployeeData(Int32 id);

    }
}
