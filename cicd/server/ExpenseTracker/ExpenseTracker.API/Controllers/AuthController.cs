using ExpenseTracker.Application.Common;
using ExpenseTracker.Application.Features.Auth.Register;
using ExpenseTracker.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
                _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterRequest request)
        {
            var result = await _authService.AuthenticateUser(request);
            var response = ApiResponse<RegisterResponse>.FromResult(result);
            return Ok(response);
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Auth Controller is working!");
        }
    }
}
