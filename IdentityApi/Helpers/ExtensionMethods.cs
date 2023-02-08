using System;
using Authentication.Entities;
using Authentication.Services.Operations;
using IdentityApi.Respository;
using IdentityApi.Respository.interfaces;
using IdentityApi.Respository.operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApi.Helpers
{
    public static class ExtensionMethods
    {
        public static void AddError(this ModelStateDictionary modelState, string errorMessage)
        {
            modelState.AddModelError("", errorMessage);
        }

        public static void DependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //
            services.AddScoped<TokenServices>();
            services.AddScoped<AccountManagerServices>();
            services.AddScoped<RolesManagerServices>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
            //services.AddHttpContextAccessor();
           // services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
           //services.AddScoped<IUserClaimsPrincipalFactory<MyUser>, MyUserClaimsPrincipalFactory>();
            services.AddScoped<IUrlHelper>(x =>
          {
              var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;//
              var factory = x.GetRequiredService<IUrlHelperFactory>();//
              return factory.GetUrlHelper(actionContext);//
          });
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