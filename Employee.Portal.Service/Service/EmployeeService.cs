using AutoMapper;
using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Repo;
using Employee.Portal.Service.Dto;
using Employee.Portal.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Portal.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Domain.Entities.Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Domain.Entities.Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task CreateEmployee(EmployeeRequest request)
        {
            var employee = new Domain.Entities.Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                BirthDate = request.BirthDate,
                DepartmentId = request.DepartmentId,
                Phone = request.Phone
            };

            await _employeeRepository.Insert(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetEmployees()
        {
            var employees = await _employeeRepository.Get(null,null, "Department");
            var results = employees.GroupBy(
                            e => e.Department.Name,
                            e => e,
                            (key, g) => new { Department = key, Employees = _mapper.Map<IEnumerable<EmployeeDto>>(g.ToList()) }); 

            return results;
        }
    }
}
