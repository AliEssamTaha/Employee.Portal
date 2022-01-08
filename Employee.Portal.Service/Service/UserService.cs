using AutoMapper;
using Employee.Portal.CoreLib.Exceptions;
using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Service.Dto;
using Employee.Portal.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Employee.Portal.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUser(string userName, string password)
        {
            var user = await _userRepository.FindByNameAsync(userName);
            if (user == null)
            {
                throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { "User not found." });
            }

            bool isCorrectPassword = await _userRepository.CheckPasswordAsync(user, password);
            if (!isCorrectPassword)
            {
                throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { "Password not matched." });
            }

            return _mapper.Map<UserDto>(user);
        }
        public async Task RegisterUser(RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { "Password does not match confirm password." });
            }

            User registrationUser = new User()
            {
                Email = request.Email,
                UserName = request.Username
            };
            IdentityResult result = await _userRepository.CreateAsync(registrationUser, request.Password);
            if (!result.Succeeded)
            {
                IdentityErrorDescriber errorDescriber = new IdentityErrorDescriber();
                IdentityError primaryError = result.Errors.FirstOrDefault();

                if (primaryError.Code == nameof(errorDescriber.DuplicateEmail))
                {
                    throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { "Email already exists." });

                }
                else if (primaryError.Code == nameof(errorDescriber.DuplicateUserName))
                {
                    throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { "Username already exists." });
                }

                throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { primaryError.Code });
            }
        }

    }
}
