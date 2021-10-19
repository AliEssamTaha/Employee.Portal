using Employee.Portal.CoreLib.Responses;
using Employee.Portal.Service.Dto;
using System;
using System.Threading.Tasks;

namespace Employee.Portal.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserResponse> Authenticate(UserDto user);
        Task Logout(Guid? userId);
        Task<AuthenticatedUserResponse> RefreshUserToken(string token);
    }
}
