using Employee.Portal.CoreLib.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Employee.Portal.API.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public Guid? UserId
        {
            get
            {
                string rawUserId = HttpContext.User.FindFirstValue("id");

                if (!Guid.TryParse(rawUserId, out Guid userId))
                {
                    return null;
                }
                return userId;
            }
        }
        protected IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new ErrorResponse(errorMessages));
        }
    }
}
