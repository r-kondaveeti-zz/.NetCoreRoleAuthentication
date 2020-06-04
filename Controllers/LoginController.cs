using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Order_Management.Domain;
using Order_Management.Identity.Helper;
using Order_Management.Identity.JwtHelper;
using Order_Management.Identity.Model;
using Order_Management.Identity.Repository;
using Order_Management.ResponseModel;

namespace Order_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController: ControllerBase
    {
        private ApplicationDbContext _dbContext;
        private IUserRepository _userRepository;
        private IEncrypter _encrypter;
        private IConfiguration _configuration;

        public LoginController(ApplicationDbContext dbContext, IUserRepository userRepository, IEncrypter encrypter, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _encrypter = encrypter;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if(login.Email == null || login.Password == null) { return Ok(new ApiResponse { Message = "email or password is null", Data = null }); }

            User user = _userRepository.GetUser(login.Email);

            if(user == null) { return Ok(new ApiResponse { Message = "user not found", Data = null }); }
            if (!user.ValidatePassword(login.Password, _encrypter)) { return Ok(new ApiResponse { Message = "wrong password", Data = null }); }

            //generate token
            JwtToken token;
            if (user.Role.Equals("admin"))
            {
                token = new JwtTokenBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                    .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                    .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                    .AddExpiry(1)
                    .AddSubject(user.Email)
                    .AddClaim("Name", "Admin")
                    .AddClaim("TenantId", user.TenantId)
                    .AddRole("Admin")
                    .Build();
            } else
            {
                token = new JwtTokenBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create(_configuration.GetValue<string>("JwtSecretKey")))
                    .AddIssuer(_configuration.GetValue<string>("JwtIssuer"))
                    .AddAudience(_configuration.GetValue<string>("JwtAudience"))
                    .AddExpiry(1)
                    .AddSubject(user.Email)
                    .AddClaim("Name", "Admin")
                    .AddClaim("TenantId", user.TenantId)
                    .AddRole("User")
                    .Build();
            }
            
            

            return Ok(new TokenApiResponse { Message = "successfully logged in", Token = token }); //return token
        }

    }

    public class LoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
