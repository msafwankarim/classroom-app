using Microsoft.AspNetCore.Mvc;
using URSBackend.Models;
using URSBackend.Models.ViewModels;
using URSBackend.Services;

namespace URSBackend.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AdminLoginController : ControllerBase
    {
        public LoginService loginService;

        public AdminLoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserVM userVM)
        {
            User user = loginService.AdminAuth(userVM);
            if (user == null)
                return Unauthorized();
            else 
                return Ok(user);
        }

        [HttpPost("student")]
        public IActionResult StudentLogin([FromBody] UserVM uservm)
        {
            StudentVM user = loginService.StudentAuth(uservm);
            if (user == null)
                return Unauthorized();
            else
                return Ok(user);
        }
    }
}