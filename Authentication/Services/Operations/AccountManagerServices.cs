using System;
using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Authentication.Services.Operations
{
    [Controller]
    public class AccountManagerServices : IAccountManagerServices
    {
        private readonly TokenServices _tokenServices;
        private readonly UserManager<MyUser> _userManager;
        private readonly IUrlHelper _url;
        public AccountManagerServices(
        UserManager<MyUser> userManager,
        SignInManager<MyUser> signInManager,
        TokenServices tokenServices,
        IUrlHelper url
        )
        {
            _userManager = userManager;
            _tokenServices = tokenServices;
            _url = url;
        }

        public async Task<UserToken> RegisterUser(MyUserDto user)
        {
            if (user == null) throw new Exception("Objeto era nulo");

            var MyUser = new MyUser()
            {
                UserName = user.UserName,
                Email = user.Email,
            };

            var register = await _userManager.CreateAsync(MyUser, user.Password);

            if (!register.Succeeded)
            {
                throw new Exception("Erro");
            }
            //await _signInManager.SignInAsync(MyUser, false);
            var myUser = await _userManager.Users
            .FirstOrDefaultAsync(x => x.NormalizedUserName == MyUser.UserName.ToUpper());

            if (register.Succeeded)
            {

                var tokenStr = await _userManager.GenerateEmailConfirmationTokenAsync(myUser);

                var urlConfirmMail = _url.Action("ConfirmEmailAddress", "auth", new { token = tokenStr, email = myUser.Email }).Remove(0, 29);

                var email = new Email();

                email.SendEmail("smtp.nostopti.com.br", "marcus@nostopti.com.br", "Nsti@2023",

                myUser.Email, "Assunto", "http://localhost:4200/confirm-email/" + urlConfirmMail);

            }
            return await _tokenServices.GenerateToken(myUser);
        }
        public async Task<bool> RetryConfirmEmailGenerateNewToken(RetryConfirmPasswordDto retryConfirmPassword)
        {
            if (retryConfirmPassword == null) throw new Exception("Objeto era nulo");

            var myUser = await _userManager.Users
                     .FirstOrDefaultAsync(x => x.NormalizedEmail == retryConfirmPassword.Email.ToUpper());

            if (myUser.EmailConfirmed)
                throw new Exception("Email já foi confirmado.");

            if (myUser != null)
            {
                var tokenStr = await _userManager.GenerateEmailConfirmationTokenAsync(myUser);

                var urlConfirmMail = _url.Action("ConfirmEmailAddress", "auth",
                new { token = tokenStr, email = myUser.Email }).Remove(0, 29);

                var emailSender = new Email();

                emailSender.SendEmail("smtp.nostopti.com.br", "marcus@nostopti.com.br", "Nsti@2023",

                myUser.Email, "Assunto", "http://localhost:4200/confirm-email/" + urlConfirmMail);

                return true;
            }

            return false;
        }
        public async Task<UserToken> Login(MyUserDto user)
        {
            var myUser = await _userManager.FindByNameAsync(user.UserName);

            if (myUser != null && !await _userManager.IsLockedOutAsync(myUser))
            {

                if (!await _userManager.IsEmailConfirmedAsync(myUser))
                    throw new Exception("Email precisa ser confirmado.");

                // var result = await _signInManager.CheckPasswordSignInAsync(myUser, user.Password, false);
                if (await _userManager.CheckPasswordAsync(myUser, user.Password))
                {
                    var checkedUser = await _userManager.Users
                    .FirstOrDefaultAsync(x => x.NormalizedUserName == myUser.UserName.ToUpper());

                    await _userManager.ResetAccessFailedCountAsync(checkedUser);

                    if (await _userManager.GetTwoFactorEnabledAsync(checkedUser))
                    {
                        var validator = await _userManager.GetValidTwoFactorProvidersAsync(checkedUser);

                        if (validator.Contains("Email"))
                        {

                            var token = await _userManager.GenerateTwoFactorTokenAsync(checkedUser, "Email");

                            var emailSender = new Email();

                            emailSender.SendEmail("smtp.nostopti.com.br", "marcus@nostopti.com.br", "Nsti@2023",

                            myUser.Email, "SONNY: Autenticação de dois fatores", "Código: Autenticação de dois fatores: " + token);

                            var returnUsrToken = await _tokenServices.GenerateToken(checkedUser);
                            returnUsrToken.Action = "TwoFactor";
                            return returnUsrToken;
                        }
                    }

                    return await _tokenServices.GenerateToken(checkedUser);
                }
                else
                {
                    await _userManager.AccessFailedAsync(myUser);
                }
            }

            if (await _userManager.IsLockedOutAsync(myUser))
            {
                var emailSender = new Email();

                emailSender.SendEmail("smtp.nostopti.com.br", "marcus@nostopti.com.br", "Nsti@2023",

                myUser.Email, "Sonny conta bloqueada.", "O número de dez tentativas de login foi esgotado e a conta foi bloqueada por atingir dez tentativas com senhas incorretas. Sugerimos troque sua senha. " + "Link para troca  de senha.");

                throw new Exception("Usuário está bloqueado.");
            }

            throw new Exception("Nome de usuário ou senha inválidos.");
        }
        public async Task<bool> ForgotPassword(ForgotPasswordDto forgotPassword)
        {


            var myUser = await _userManager.
            FindByEmailAsync(forgotPassword.Email);
            // var myUser = await _userManager.
            // FindByNameAsync(forgotPassword.UserName);

            if (myUser != null)
            {

                var token = await _userManager.GeneratePasswordResetTokenAsync(myUser);

                var urlReset = _url.Action("Reset", "auth", new { token = token, email = myUser.Email }).Remove(0, 15);

                var email = new Email();

                email.SendEmail("smtp.nostopti.com.br", "marcus@nostopti.com.br", "Nsti@2023",
                                           myUser.Email, "Assunto", "http://localhost:4200/reset-password/" + urlReset);

                return true;
            }
            else
            {
                return false;
            }
        }
        public ResetPasswordDto ResetPassword(string token, string email)
        {
            return new ResetPasswordDto { Token = token, Email = email };
        }
        public async Task<bool> ResetPassword(ResetPasswordDto resetPassword)
        {
            MyUser myUser = await _userManager.FindByEmailAsync(resetPassword.Email);

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(myUser, resetPassword.Token, resetPassword.Password);

            if (!identityResult.Succeeded)
            {
                string ErrorToReturn = string.Empty;

                foreach (var err in identityResult.Errors)
                {
                    ErrorToReturn += err.Description;
                }

                throw new Exception(ErrorToReturn);
            }

            return true;

        }
        public async Task<bool> ConfirmEmailAddress(ConfirmEmailDto confirmEmail)
        {
            var myUserFromDb = await _userManager.FindByEmailAsync(confirmEmail.Email);

            if (myUserFromDb.EmailConfirmed)
                throw new Exception("Email já foi confirmado.");

            if (myUserFromDb != null)
            {
                var result = await _userManager.ConfirmEmailAsync(myUserFromDb, confirmEmail.Token);
                if (result.Succeeded)
                {
                    return true;
                }
            }

            return false;
        }
        public async Task<UserToken> TwoFactor(T2FactorDto t2Factor)
        {
            var user = await _userManager.FindByNameAsync(t2Factor.UserName);

            if (user == null)
                throw new Exception("Autenticação inválida!");

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", t2Factor.Token);

            if (!isValid)
                throw new Exception("Invalid token verification.");

            return await _tokenServices.GenerateToken(user);
        }

        Task<bool> IAccountManagerServices.ResetPassword(string token, string email)
        {
            throw new NotImplementedException();
        }
    }
}