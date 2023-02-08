using System;
using System.Reflection;
using System.Text;
using IdentityApi.Helpers;
using IdentityApi.Respository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace IdentityApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityApi", Version = "v1" });
            });

            IdentityExtensionMethods.AddIdentity(services);
            IdentityExtensionMethods.DataProtectionTokenProviderOptions(services);
            ExtensionMethods.DependencyInjection(services);

            // services.AddAuthentication();



    services.AddAuthorization(options =>
        options.AddPolicy("TwoFactorEnabled",
            x => x.RequireClaim("amr", "sub")));



            
            services.AddAuthentication(x =>
                     {
                         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                     }).AddJwtBearer(x =>
                     {
                         x.RequireHttpsMetadata = false;
                         x.SaveToken = false;
                         x.TokenValidationParameters = new TokenValidationParameters()
                         {
                             ValidateIssuerSigningKey = true,
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"])),
                             ValidateAudience = false,
                             ValidateIssuer = false,
                             ValidateLifetime = true,
                             ClockSkew = TimeSpan.Zero
                         };
                     });

            IdentityExtensionMethods.AddAuthorizeAllControllers(services);

            services.AddCors();

            string cxStr = Configuration.GetConnectionString("IdentityDb");

            var migrationAssembly = typeof(Startup).GetTypeInfo()
            .Assembly.GetName().Name;

            services.AddDbContext<IdDbContext>(db =>
             db.UseMySql(cxStr, ServerVersion.AutoDetect(cxStr), migration =>
             migration.MigrationsAssembly(migrationAssembly)));

            services.AddHttpContextAccessor();

            // services.Configure<CookiePolicyOptions>(opt =>
            // {
            //     opt.CheckConsentNeeded = context => true;
            //     opt.MinimumSameSitePolicy = SameSiteMode.None;
            // });

            // services.ConfigureApplicationCookie(opt => 
            // opt.LoginPath = "/Home/Login"
            // );

           
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityApi v1"));
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
