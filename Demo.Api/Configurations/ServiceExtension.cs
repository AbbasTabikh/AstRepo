using Demo.Api.Services;
using Demo.Data.Configurations;
using Demo.Data.Data;
using Demo.Data.Models;
using Demo.Utils.RecaptchaV3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Demo.Api.Configurations
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddJwt(this IServiceCollection services , IConfiguration configurations)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations.GetSection("JwtKey:SecretKey").Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services , IConfiguration configurations)
        {
            services.AddScoped<SignInManager<ApplicationUser>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IMunicipalityService, MunicipalityService>();


            services.AddSingleton(GetRecaptchaConfiguration(configurations.GetSection(nameof(RecaptchaConfiguration))));
            //services.AddScoped<RecaptchaConfiguration , RecaptchaConfiguration>();
            services.AddScoped<IRecaptchaService, RecaptchaService>();

            return services;
        }
        public static IServiceCollection AddIdentityCore(this IServiceCollection services)
        {
            //this should be added inorder for the AddDefaultTokenProviders to work
            services.AddDataProtection();

            //this should be added for the SignInManager 
            services.AddHttpContextAccessor();

            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<DataContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                 {       
                    new OpenApiSecurityScheme
                    {
                     Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
                }
                });
            });

            return services;
        }



        private static RecaptchaConfiguration GetRecaptchaConfiguration(IConfigurationSection configurationSection)
        {
            var secretKey = configurationSection["RecaptchaSecretKey"];
            var url = configurationSection["RecaptchaVerificationURL"];
            return new RecaptchaConfiguration(secretKey, url);
        }
    }
}
