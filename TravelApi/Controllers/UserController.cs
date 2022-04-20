using Microsoft.AspNetCore.Mvc;
using TravelApi.Models;
using TravelApi.Services;
using System;

namespace TravelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                {
                return BadRequest(new { message = "Username or password is incorrect" });
                }
            Console.WriteLine("**************** Made it past the BadRequestLine ******");
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}