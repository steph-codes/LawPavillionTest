using LawPavillionTest.Domain.DTOs.Request;
using LawPavillionTest.Domain.DTOs.Response;
using LawPavillionTest.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LawPavillionTest.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;
        //private readonly UserManager<AppUser> _userManager;
       
        string checkForEmail = "@";

        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromForm] RegisterModel request)
        {
            try
            {
                if (request.Email == null || request.PhoneNumber == null || request.Password == null)
                {
                    return BadRequest("Fill in Email, Phonenumber and Password");
                }

                var authResponse = await _authService.RegisterAsync(request);
                return Ok(authResponse);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel request)
        {
            try
            {

                if (request.Username == null || request.Password == null)
                {
                    return BadRequest("Please fill in Username and Password");
                }

                var authResponse = await _authService.LoginAsync(request);
                return Ok(authResponse);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
