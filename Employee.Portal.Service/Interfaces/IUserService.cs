using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Service.Dto;
using System.Threading.Tasks;

namespace Employee.Portal.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUser(string userName, string password);
        Task RegisterUser(RegisterRequest registerRequest);
    }
}
