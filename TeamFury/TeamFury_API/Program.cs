using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.DTOs;
using TeamFury_API.Data;


namespace TeamFury_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
                Title = "Minimal API - JWT Authentication with Swagger demo"
            };

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", info);
                o.AddSecurityDefinition("Bearer", securityScheme);
                o.AddSecurityRequirement(securityReq);
            });

            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();


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


            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.MapPost("/security/getToken", [AllowAnonymous](LoginDTO user) =>
            {
                if (user.Username == "Admin2" && user.Password == "MTG15")
                {
                    var issuer = builder.Configuration["Jwt:Issuer"];
                    var audience = builder.Configuration["Jwt:Audience"];
                    var securityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    // Now its ime to define the jwt token which will be responsible of creating our tokens
                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    // We get our secret from the appsettings
                    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

                    // we define our token descriptor
                    // We need to utilise claims which are properties in our token which gives information about the token
                    // which belong to the specific user who it belongs to
                    // so it could contain their id, name, email the good part is that these information
                    // are generated by our server and identity framework which is valid and trusted
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", "1"),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                            new Claim(JwtRegisteredClaimNames.Email, user.Username),
                            // the JTI is used for our refresh token which we will be convering in the next video
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        }),
                        // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
                        // but since this is a demo app we can extend it to fit our current need
                        Expires = DateTime.UtcNow.AddHours(6),
                        Audience = audience,
                        Issuer = issuer,
                        // here we are adding the encryption alogorithim information which will be used to decrypt our token
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature)
                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescriptor);

                    var jwtToken = jwtTokenHandler.WriteToken(token);

                    return Results.Ok(jwtToken);
                }
                else
                {
                    return Results.Unauthorized();
                }
            });
            
            app.MapGet("/api/admin/", [Authorize] async (AppDbContext db) =>
                Results.Ok(await db.Admins.ToListAsync()));

            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}