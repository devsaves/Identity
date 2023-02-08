using System;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Dto;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Services.Operations
{
    public interface IAccountManagerServices
    {
        // Task<UserToken> RegisterUser([FromBody] MyUserDto user);
        // Task<UserToken> Login(MyUserDto user);


        Task<UserToken> RegisterUser(MyUserDto user);
        Task<bool> RetryConfirmEmailGenerateNewToken(RetryConfirmPasswordDto retryConfirmPassword);
        Task<UserToken> Login(MyUserDto user);
        Task<bool> ForgotPassword(ForgotPasswordDto forgotPassword);
        Task<bool> ResetPassword(string token, string email);
        Task<bool> ResetPassword(ResetPasswordDto resetPassword);
        Task<bool> ConfirmEmailAddress(ConfirmEmailDto confirmEmail);
        Task<UserToken> TwoFactor(T2FactorDto t2Factor);

    }
}