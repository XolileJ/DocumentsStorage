using DocumentsStorage.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DocumentsStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public readonly IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Post(string username, string password)
        {
            try
            {
                return Ok(userService.Authenticate(username, password));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}