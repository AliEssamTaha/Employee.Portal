using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Service.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Portal.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(EmployeeRequest request, Guid createdBy);
        Task<bool> UpdateEmployee(EmployeeRequest request, Guid modifiedBy);
        Task<bool> DeleteEmployee(int id);
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployees();
    }
}