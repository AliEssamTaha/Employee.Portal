using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Create New Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var result = await _employeeService.CreateEmployee(request, UserId.Value);

            return Ok(result);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] EmployeeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var result = await _employeeService.UpdateEmployee(request, UserId.Value);

            return Ok(result);
        }

        /// <summary>
        /// Delete Employee By id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            var result = await _employeeService.DeleteEmployee(Id);

            return Ok(result);
        }

        /// <summary>
        /// Get Employee details by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            return Ok(await _employeeService.GetEmployeeById(Id));
        }

        /// <summary>
        /// Get All Employee list
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetEmployees());
        }
    }
}
