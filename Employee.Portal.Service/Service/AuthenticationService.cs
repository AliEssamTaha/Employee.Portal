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
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IAccessTokenGenerator accessTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IRepository<RefreshToken> refreshTokenRepository,
            IUserService userService, 
            IMapper mapper
            )
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task Logout(Guid? userId)
        {
            if (!userId.HasValue)
            {
                throw new AppCustomException(HttpStatusCode.Unauthorized, new List<string> { "User not found." });
            }

            var refreshRecords = await _refreshTokenRepository.Get(o => o.UserId == userId);

            _refreshTokenRepository.RemoveRange(refreshRecords);
            
            await _refreshTokenRepository.SaveChangesAsync();
        }
        public async Task<AuthenticatedUserResponse> RefreshUserToken(string token)
        {
            var user = await _userService.GetUserByRefreshToken(token);

            if(user == null)
            {
                throw new AppCustomException(HttpStatusCode.BadRequest, new List<string> { "User not found." });
            }
            _refreshTokenRepository.Remove(user.RefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return await Authenticate(_mapper.Map<UserDto>(user));
        }
        public async Task<AuthenticatedUserResponse> Authenticate(UserDto user)
        {
            var accessToken = _accessTokenGenerator.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken()
            {
                Token = refreshToken,
                UserId = user.Id,
                CreatedBy = user.Id
            };
            var checkExistToken = await _refreshTokenRepository.Get(o => o.UserId == user.Id);
            if (checkExistToken.Any())
            {
                _refreshTokenRepository.RemoveRange(checkExistToken);
            }
            await _refreshTokenRepository.Insert(refreshTokenDTO);
            await _refreshTokenRepository.SaveChangesAsync();

            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken.Value,
                AccessTokenExpirationTime = accessToken.ExpirationTime,
                RefreshToken = refreshToken
            };
        }
    }
}
