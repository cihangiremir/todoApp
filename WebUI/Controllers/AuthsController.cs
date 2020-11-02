using Business.Abstract;
using Entities.Dto.AppUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Login then create to token.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "email": test@appcent.com,
        ///        "password": appcentdemo
        ///     }
        ///
        /// </remarks>
        /// <param name="loginInput"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUserLoginInput loginInput)
        {
            var result = await _authService.Login(loginInput);

            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="registerInput"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserRegisterInput registerInput)
        {
            var result = await _authService.Register(registerInput);
            if (!result.Success) return BadRequest(result.Message);

            return CreatedAtAction("Login", result.Data);
        }
    }
}
