using System.Text;
using System.Text.RegularExpressions;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;
using TeamFury_API.Endpoints;
using TeamFury_API.MappingProfiles;
using TeamFury_API.Services;
using TeamFury_API.Services.AdminServices;
using TeamFury_API.Services.EmailServices;
using TeamFury_API.Services.SecurityServices;


namespace TeamFury_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            #region Service Container

            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };
            
             var client = new SecretClient(new Uri(builder.Configuration["KeyVaultConfig:KeyVaultURL"]),
                new DefaultAzureCredential(), options);
            KeyVaultSecret kvs = client.GetSecret("Default");
            
            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(kvs.Value));
            
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ILeaveDaysService, LeaveDaysService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddAutoMapper(typeof(UserConfig));
            builder.Services.AddAutoMapper(typeof(RequestConfig));
            builder.Services.AddAutoMapper(typeof(RequestTypeConfig));
            builder.Services.AddAutoMapper(typeof(RequestLogConfig));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            #endregion

            #region Swagger Configuration.

            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JSON Web Token based security",
            };
            var securityReq = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            };
            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Minimal API - JWT Authentication with Swagger"
            };

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", info);
                o.AddSecurityDefinition("Bearer", securityScheme);
                o.AddSecurityRequirement(securityReq);
            });

            #endregion
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default",
                    policy => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
            });
            #region Authentication/Authorization options.

            KeyVaultSecret kvAd = client.GetSecret("IssuerAdu");
            KeyVaultSecret kvJwt = client.GetSecret("AuthKey");
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = kvAd.Value,
                    ValidAudience = kvAd.Value,
                    IssuerSigningKey = new SymmetricSecurityKey
                                                (Encoding.UTF8.GetBytes(kvJwt.Value)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsAdmin", policy => { policy.RequireRole("ADMIN"); });
                opt.AddPolicy("Dev", policy => { policy.RequireRole("Dev");});
            });

            #endregion


            #region Identity requirments config.

            builder.Services.Configure<IdentityOptions>(options =>
            {
                //Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;

                //Lockout Settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //User Settings
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            });

            #endregion

            // Build to app.
            var app = builder.Build();

            app.UseCors("default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.AddSecurityEndpoint();
            app.AdminEndpointConfig();
            app.UserEndpointConfig();
            app.SharedEndpointsConfig();

            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}
