using System;
using Microsoft.AspNetCore.Mvc;
using Order_Management.Identity.Service;
using Order_Management.ResponseModel;

namespace Order_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController: ControllerBase
    {
        IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserBinding user)
        {
            if (user.Name == null || user.Email == null || user.Password == null || user.Role == null || user.TenantId == null)
            {
                return BadRequest();
            }

            try
            {
                _userService.RegisterUser(user.Name, user.Email, user.Password, user.Role, user.TenantId);
            } catch(Exception e)
            {
                Console.WriteLine("User Id Exists");
                return Ok(new ApiResponse { Message = "email is already registered", Data = null });
            }
            
            return Ok(new ApiResponse { Message = "user registered", Data = user });
        }
    }

    public class UserBinding
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string TenantId { get; set; }
    }
}
