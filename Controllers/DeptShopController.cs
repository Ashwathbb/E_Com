using Department.DataAcess.Dto;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.DTOs;
using Shop.DataAccess.Models;
using Shop.Service.IService;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeptShopController : ControllerBase
    {
        private readonly IDepartmentServiceClient _departmentServiceClient;
        private readonly IShopService _shopService;

        public DeptShopController(IDepartmentServiceClient departmentServiceClient, IShopService shopService)
        {
            _departmentServiceClient = departmentServiceClient;
            _shopService = shopService;
        }
        [HttpGet("department/{departmentGuid}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(Guid departmentGuid)
        {
            DepartmentDto department = await _departmentServiceClient.GetDepartmentByGuidAsync(departmentGuid);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentServiceClient.GetAllDepartmentsAsync();
            return Ok(departments);
        }
        [HttpPost("add-department")]
        public async Task<ActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            await _departmentServiceClient.AddDepartmentAsync(departmentDto);
            return Ok();
        }
        [HttpGet("users-with-departments")]
        public async Task<ActionResult<IEnumerable<UserDepartmentDto>>> GetUsersWithDepartments()
        {
            var userDepartments = await _shopService.GetUsersWithDepartmentsAsync();
            return Ok(userDepartments);
        }
        [HttpPost("add-user-department")]
        public async Task<IActionResult> AddUserAndDepartment([FromBody] AddUserAndDepartmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usersInfoDto = new UsersInfoDepartment
            {
                UserId = request.UserId,
                UsersInfoGuid = request.UsersInfoGuid,
                UserName = request.UserName,
                DepartmentGuid = request.DepartmentGuid
            };

            var departmentDto = new DepartmentDto
            {
                DepartmentGuid = request.DepartmentGuid,
                DepartmentName = request.DepartmentName
            };

            await _shopService.AddUserAndDepartmentAsync(usersInfoDto, departmentDto);

            return Ok("User and Department added successfully.");
        }

    }
}
