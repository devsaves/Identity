using System;
using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Services;
using Authentication.Services.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AccountManagerServices _accountManagerServices;
        public AuthController(AccountManagerServices accountManagerServices)
        {
            _accountManagerServices = accountManagerServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(MyUserDto user)
        {

            var result = await _accountManagerServices.RegisterUser(user);

            if (!result.Authenticated)
            {
                throw new Exception("Erro na tentativa de criar o usuário.");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] MyUserDto user)
        {
            var login = await _accountManagerServices.Login(user);

            if (login.Authenticated)
            {
                return Ok(login);
            }

            return Unauthorized();
        }

        [HttpGet]
        public ResetPasswordDto Reset(string token, string email)
        {
            ResetPasswordDto result = _accountManagerServices.ResetPassword(token, email);
            return result;
        }

        [HttpPost("Reset")]
        public async Task<IActionResult> Reset(ResetPasswordDto resetPassword)
        {
            if (resetPassword == null) throw new Exception("Objeto era nulo");
            return Ok(await _accountManagerServices.ResetPassword(resetPassword));
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            if (forgotPassword == null) throw new Exception("Objeto era nulo.");

            if (!await _accountManagerServices.ForgotPassword(forgotPassword)) throw new Exception("Objeto era nulo."); //BadRequest("Usuário não encontrado");


            return Ok(true);
        }
        [HttpPost("RetryConfirmEmailGenerateNewToken")]
        public async Task<IActionResult> RetryConfirmEmailGenerateNewToken(RetryConfirmPasswordDto retryConfirmPassword)
        {
            if (retryConfirmPassword == null) throw new Exception("Objeto era nulo.");

            if (!await _accountManagerServices.RetryConfirmEmailGenerateNewToken(retryConfirmPassword)) throw new Exception("Objeto era nulo."); //BadRequest("Usuário não encontrado");


            return Ok(true);
        }

        [HttpPost("ConfirmEmailAddress")]
        public async Task<IActionResult> ConfirmEmailAddress(ConfirmEmailDto confirmEmail)
        {
            if (confirmEmail == null) throw new Exception("Objeto era nulo.");

            if (!await _accountManagerServices.ConfirmEmailAddress(confirmEmail)) throw new Exception("Objeto era nulo."); //BadRequest("Usuário não encontrado");

            return Ok(true);
        }

        [HttpPost("TwoFactor")]
        public async Task<IActionResult> TwoFactor([FromBody] T2FactorDto t2Factor)
        {
            var result = await _accountManagerServices.TwoFactor(t2Factor);

            return Ok(result);
        }



        


    }
}
