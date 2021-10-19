using Employee.Portal.CoreLib.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Portal.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateEmployee(EmployeeRequest request);
        Task<IEnumerable<object>> GetEmployees();
    }
}