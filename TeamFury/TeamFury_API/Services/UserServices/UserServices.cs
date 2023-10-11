using Microsoft.AspNetCore.Identity;
using Models.Models;

namespace TeamFury_API.Services.UserServices;

public class UserServices : IUserServices
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task CreateUserAsync()
    {
        var admin = await _userManager.FindByNameAsync("User1");

        if (admin == null)
        {
            var newUser = new User
            {
                Email = "User@user.com",
                UserName = "User1",
                PhoneNumber = "0394837463",
                EmailConfirmed = true
            };

            var createAdmin = await _userManager.CreateAsync(newUser, "P@ssword1!");

            if (createAdmin.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("User"))
                    await _userManager.AddToRoleAsync(newUser, "User");
            }
        }
        
        
    }
}