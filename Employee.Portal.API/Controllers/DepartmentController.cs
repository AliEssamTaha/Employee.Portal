using Employee.Portal.CoreLib.Requests;
using Employee.Portal.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Employee.Portal.API.Controllers
{
    [Authorize]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] DepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            await _departmentService.CreateDepartment(request);
            
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetDepartments());
        }
    }
}
