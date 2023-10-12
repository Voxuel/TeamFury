using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

    #region Employee Commands
    
    /// <summary>
    /// Gets all employees in the database.
    /// </summary>
    /// <returns>IEnumerable of all employees</returns>
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userManager.Users.ToListAsync();
    }

    /// <summary>
    /// Gets single entity from database.
    /// </summary>
    /// <param name="id">Employee Id</param>
    /// <returns>Task of type: <see cref="User"/></returns>
    public async Task<User> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }
    
    /// <summary>
    /// Updates existing employee in the database
    /// </summary>
    /// <param name="newUpdate">Object of type <see cref="User"/></param>
    /// <param name="password">Password contained in newUpdate object</param>
    /// <returns>Task of type: <see cref="User"/></returns>
    public async Task<User> UpdateAsync(User newUpdate, string password)
    {
        var found = await _userManager.FindByIdAsync(newUpdate.Id);
        if (found == null) return null;

        if (!string.IsNullOrEmpty(password))
        {
            var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(found);
            await _userManager.ResetPasswordAsync(found, passwordToken, password);
        }

        found.UserName = newUpdate.UserName;
        found.NormalizedUserName = newUpdate.UserName.ToUpper();
        found.Email = newUpdate.Email;
        found.NormalizedUserName = newUpdate.Email.ToUpper();
        found.PhoneNumber = newUpdate.PhoneNumber;
        
        
        await _userManager.UpdateAsync(found);

        return found;
    }
    
    /// <summary>
    /// Deletes employee with given ID in the database.
    /// </summary>
    /// <param name="id">ID of employee to delete from record</param>
    /// <returns>Task of type: <see cref="User"/></returns>
    public async Task<User> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return null;
        
        var userDeleted = await _userManager.DeleteAsync(user);
        
        return !userDeleted.Succeeded ? null : user;
    }

    /// <summary>
    /// Creates a new entity and adds it to the database of type Employee / <see cref="User"/>
    /// </summary>
    /// <param name="user">Object to create</param>
    /// <returns>Task of type: <see cref="User"/></returns>
    public async Task<User> CreateAsync(User user, string role)
    {
        var userFound = await _userManager.FindByNameAsync(user.UserName);

        if (userFound != null) return null;
        
        user.SecurityStamp = Guid.NewGuid().ToString();
        var createUserResult = await _userManager.CreateAsync(user);

        if (!createUserResult.Succeeded) return null;
        if (await _roleManager.RoleExistsAsync(role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        return user;
    }

    
    // Save for azure db migration.
    public async Task CreateRoleAsync()
    {
        if (!await _roleManager.RoleExistsAsync("Employee"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Employee"));
        }
        else
        {
            Console.WriteLine("Role exists for employee");
        }
        
        if (!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        else
        {
            Console.WriteLine("Role exists for admin");
        }
    }
    
    #endregion
    
    #region Overridden methods
    public Task<User> UpdateAsync(User newUpdate)
    {
        throw new NotImplementedException();
    }
    public Task<User> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateAsync(User toCreate)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByID(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}