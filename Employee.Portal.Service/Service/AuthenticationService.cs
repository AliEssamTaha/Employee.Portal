using AutoMapper;
using Employee.Portal.CoreLib.Exceptions;
using Employee.Portal.CoreLib.Responses;
using Employee.Portal.Domain.Entities;
using Employee.Portal.Repo;
using Employee.Portal.Service.Interfaces;
using Employee.Portal.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Employee.Portal.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IAccessTokenGenerator accessTokenGenerator,
            IUserService userService, 
            IMapper mapper
            )
        {
            _accessTokenGenerator = accessTokenGenerator;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<AuthenticatedUserResponse> GenerateToken(UserDto user)
        {
            var accessToken = _accessTokenGenerator.GenerateToken(user);

            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken.Value,
                AccessTokenExpirationTime = accessToken.ExpirationTime
            };
        }

    }
}
