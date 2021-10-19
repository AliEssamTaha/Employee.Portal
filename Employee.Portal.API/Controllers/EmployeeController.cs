using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee.Portal.API.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _employeeService.CreateEmployee(request);
            
            return Ok();
        }


        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetEmployees());
        }
    }
}
