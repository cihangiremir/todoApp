using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IAppUserService _appUserService;
        public UsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _appUserService.GetListByFilter();

            if (!result.Success) return NotFound();
            return Ok(result.Data);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var result = await _appUserService.GetByFilter(t => t.Id == userId);

            if (!result.Success) return NotFound();
            return Ok(result.Data);
        }
    }
}
