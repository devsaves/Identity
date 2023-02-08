using System;
using Authentication.Entities;
using Authentication.Services.Operations;
using IdentityApi.Respository;
using IdentityApi.Respository.interfaces;
using IdentityApi.Respository.operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApi.Helpers
{
    public static class IdentityExtensionMethods
    {
        //time before expires token for password reset.
        public static void DataProtectionTokenProviderOptions(IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(
                opt => opt.TokenLifespan = TimeSpan.FromHours(1)
            );
        }
        public static void AddAuthorizeAllControllers(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        public static void AddIdentity(IServiceCollection services)
        {

            services.AddIdentity<MyUser, Role>(opt =>
             {
                 opt.SignIn.RequireConfirmedEmail = true;
                 //
                 opt.User.RequireUniqueEmail = true;
                 //
                 opt.Password.RequireDigit = false;
                 opt.Password.RequireNonAlphanumeric = false;
                 opt.Password.RequireLowercase = false;
                 opt.Password.RequireUppercase = false;
                 opt.Password.RequiredLength = 3;
                 //
                 opt.Lockout.MaxFailedAccessAttempts = 3;
                 opt.Lockout.AllowedForNewUsers = true;
             })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<IdDbContext>()
                .AddPasswordValidator<PasswordValidatorPolicies<MyUser>>()
               .AddRoleValidator<RoleValidator<Role>>()
               .AddRoleManager<RoleManager<Role>>()
               .AddSignInManager<SignInManager<MyUser>>()
               .AddDefaultTokenProviders();


        }


    }





    // services.AddIdentity<MyUser, Role>(opt =>
    //        {
    //            opt.SignIn.RequireConfirmedEmail = true;


    //            opt.Password.RequireDigit = false;
    //            opt.Password.RequireNonAlphanumeric = false;
    //            opt.Password.RequireLowercase = false;
    //            opt.Password.RequireUppercase = false;
    //            opt.Password.RequiredLength = 3;
    //        })
    //         .AddRoles<Role>()
    //        .AddEntityFrameworkStores<IdDbContext>()
    //        .AddRoleValidator<RoleValidator<Role>>()
    //        .AddRoleManager<RoleManager<Role>>()
    //        .AddSignInManager<SignInManager<MyUser>>()
    //        .AddDefaultTokenProviders();
    //         }





}