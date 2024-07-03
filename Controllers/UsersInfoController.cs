using Department.DataAcess.Dto;
using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess.DTOs;
using Shop.Service.IService;
using Shop.Service.IService.Services;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersInfoController : ControllerBase
    {
        private readonly IUsersInfoService _usersInfoService;
        private readonly IDepartmentServiceClient _departmentServiceClient;

        public UsersInfoController(IUsersInfoService usersInfoService, IDepartmentServiceClient departmentServiceClient)
        {
            _usersInfoService = usersInfoService;
            _departmentServiceClient = departmentServiceClient;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<UsersInfoDto>> GetAll()
        {
            var User = _usersInfoService.GetAllUsers();
            return Ok(User);
        }

        [HttpGet("GetById")]
        public ActionResult<UsersInfoDto> GetById(Guid usersInfoGuid)
        {
            var user = _usersInfoService.GetUserById(usersInfoGuid);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<CreateUserDto>> CreateUser([FromBody] CreateUserDto userDto)
        {
            DepartmentDto deptResult = await _departmentServiceClient.GetDepartmentByGuidAsync(userDto.DepartmentGuid);

            if (deptResult.DepartmentGuid == null)
            {
                userDto.DepartmentGuid = Guid.Empty;
            }

            var obj = new CreateUserDto()
            {
                UserName = userDto.UserName,
                EmailId = userDto.EmailId,  
                Password = userDto.Password,
                IsActive = userDto.IsActive,
                DepartmentGuid = deptResult == null ? Guid.Empty : deptResult.DepartmentGuid,
            };

            var request = _usersInfoService.CreateUser(obj);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, userDto);
        }

        [HttpPut("UpdateUser")]
        public ActionResult UpdateUser(int id, [FromBody] UsersInfoDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }
            _usersInfoService.UpdateUser(userDto);
            return NoContent();
        }

        [HttpDelete("DeleteUser")]
        public ActionResult DeleteUser(int id)
        {
            _usersInfoService.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("addUserProducts")]
        public IActionResult AddUserProducts([FromBody] UserProductSelectionDto userProductSelection)
        {
            _usersInfoService.AddUserProducts(userProductSelection);
            return Ok("Products successfully added to user");
        }
    }
}
