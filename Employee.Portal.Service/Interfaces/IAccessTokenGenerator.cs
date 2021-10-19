using Employee.Portal.Service.Models;
using Employee.Portal.Service.Dto;

namespace Employee.Portal.Service.Interfaces
{
    public interface IAccessTokenGenerator
    {
        AccessToken GenerateToken(UserDto user);
    }
}
