
using Food.Web.API.Models;
using Food.Web.API.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Food.Web.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository repo)
        {
            _accountRepository = repo;
        }
        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
           
             SignUpModel user = new SignUpModel
            {
                Email = model.Email,
                Password = model.Password,
                FirstName=model.FirstName,
                LastName=model.LastName,
                ConfirmPassword=model.ConfirmPassword
            };
             var result = await _accountRepository.SingUpAsync(user);
            if (result.Succeeded)
                return Ok(result.Succeeded);
            return Unauthorized();
        }
        [HttpPost("SignIn")]
        public async Task<ActionResult> SignIn(SignInModel model)
        {
            SignInModel user = new SignInModel
            {
                Email = model.Email,
                Password = model.Password
            };
            var result = await _accountRepository.SingInAsync(user);
            LoginSuccess login=new LoginSuccess
            {
                UserName=user.Email,
            };
            if (string.IsNullOrEmpty(result))
                return Unauthorized();
            return Ok(result);
        }
        [HttpGet("LoginSuccess")]
        public async Task<IActionResult>loginSuccess(LoginSuccess model)
        {
            return Ok(model);
        }
    }
}