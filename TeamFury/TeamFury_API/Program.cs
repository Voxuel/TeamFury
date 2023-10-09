using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TeamFury_API
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication().AddJwtBearer()
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
                        ValidAudience = builder.Configuration["LocalAuthIssuer:ValidAudiences"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            builder.Services.AddAuthorization();
            
            var app = builder.Build();

            
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.Run();
        }
    }
}