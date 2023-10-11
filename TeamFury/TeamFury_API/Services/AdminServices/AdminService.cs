using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.AdminServices;

public class AdminService : IAdminService
{
    
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    

    
    
    // Save for azure db migration.
    public async Task CreateRoleAsync()
    {
        await _roleManager.CreateAsync(new IdentityRole("Employee"));
    }

    public Task<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User newUpdate)
    {
        throw new NotImplementedException();
    }
    
    public async Task<User> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return null;
        
        var userDeleted = await _userManager.DeleteAsync(user);
        
        return !userDeleted.Succeeded ? null : user;
    }


    public async Task<User> CreateAsync(User user)
    {
        var userFound = await _userManager.FindByNameAsync(user.UserName);

        if (userFound != null) return null;
        
        var createUserResult = await _userManager.CreateAsync(user);

        if (!createUserResult.Succeeded) return null;
        if (await _roleManager.RoleExistsAsync("Employee"))
        {
            await _userManager.AddToRoleAsync(user, "Employee");
        }

        return user;
    }
    
    
    public Task<User> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}