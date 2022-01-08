using AutoMapper;
using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Repo;
using Employee.Portal.Service.Dto;
using Employee.Portal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Portal.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<EmployeeEntity> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<EmployeeEntity> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<bool> CreateEmployee(EmployeeRequest request, Guid createdBy)
        {
            var employee = new EmployeeEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                BirthDate = request.BirthDate,
                Phone = request.Phone,
                CreatedBy = createdBy
            };

            await _employeeRepository.Insert(employee);
            return await _employeeRepository.SaveChangesAsync() > 0;
        }


        public async Task<bool> UpdateEmployee(EmployeeRequest request, Guid modifiedBy)
        {
            var employee = await _employeeRepository.Get(request.Id.Value);

            if (employee == null) return false;

            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Address = request.Address;
            employee.BirthDate = request.BirthDate;
            employee.Phone = request.Phone;
            employee.ModifiedBy = modifiedBy;

            _employeeRepository.Update(employee);
            return await _employeeRepository.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.Get(id);

            if (employee == null) return false;

            _employeeRepository.Remove(employee);
            return await _employeeRepository.SaveChangesAsync() > 0;
        }


        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            var result = await _employeeRepository.Get(id);

            return _mapper.Map<EmployeeDto>(result);
        }


        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var results = await _employeeRepository.GetAll();

            return _mapper.Map<IEnumerable<EmployeeDto>>(results);
        }
    }
}
