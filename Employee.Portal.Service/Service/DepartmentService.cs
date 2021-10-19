using AutoMapper;
using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Repo;
using Employee.Portal.Service.Interfaces;
using Employee.Portal.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Portal.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task CreateDepartment(DepartmentRequest request)
        {
            var department = new Department
            {
                Name = request.Name,
                Address = request.Address
            };

            await _departmentRepository.Insert(department);
            await _departmentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartments()
        {
            return _mapper.Map<IEnumerable<DepartmentDto>>(await _departmentRepository.GetAll());
        }
    }
}
