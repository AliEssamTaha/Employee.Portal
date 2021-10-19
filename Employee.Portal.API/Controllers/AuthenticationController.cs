using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee.Portal.API.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _userService.RegisterUser(registerRequest);

            return Ok();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var user = await _userService.GetUser(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var response = await _authenticationService.Authenticate(user);

            return Ok(response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshRequest"></param>
        /// <returns></returns>

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var response = await _authenticationService.RefreshUserToken(refreshRequest.RefreshToken);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout(UserId);

            return NoContent();
        }
    }
}
