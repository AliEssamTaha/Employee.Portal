using AutoMapper;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Service.Dto;

namespace Employee.Portal.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<EmployeeEntity, EmployeeDto>().ReverseMap();
        }
    }
}
