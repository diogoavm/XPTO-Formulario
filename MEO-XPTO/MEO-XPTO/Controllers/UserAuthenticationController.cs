using MEO_XPTO.Models.Business;
using MEO_XPTO.Models.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MEO_XPTO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserAuthenticationController> _logger;

        public UserAuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<UserAuthenticationController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }

            return Ok(new { message = "Successfully logged in" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                // ...
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogError(error.Description);
                }

                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
