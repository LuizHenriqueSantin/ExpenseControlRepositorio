using ExpenseControl.Application.Interfaces;
using ExpenseControl.Application.Models.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControl.Api.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] UserIn user)
        {
            return Ok(await _userService.CreateAsync(user));
        }

        [HttpDelete]
        [Route("DeleteMyAccount")]
        public async Task<IActionResult> DeleteMyAccount()
        {
            return Ok(await _userService.DeleteCurrentUserAsync());
        }
    }
}
