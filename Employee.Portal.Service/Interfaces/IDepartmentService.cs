using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Portal.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateDepartment(DepartmentRequest departmentDto);
        Task<IEnumerable<DepartmentDto>> GetDepartments();
    }
}