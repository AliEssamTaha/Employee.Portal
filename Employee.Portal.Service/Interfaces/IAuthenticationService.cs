using Employee.Portal.CoreLib.Responses;
using Employee.Portal.Service.Dto;
using System;
using System.Threading.Tasks;

namespace Employee.Portal.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserResponse> GenerateToken(UserDto user);
    }
}
