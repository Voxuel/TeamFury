using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;

namespace TeamFury_API.Services.AdminServices;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;

    public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    #region Development Commands

    public async Task ResetLeaveDays()
    {
        var ld = await _context.LeaveDays.ToListAsync();
        foreach (var day in ld)
        {
            day.Days = 0;
        }

        await _context.SaveChangesAsync();
    }

    #endregion

    #region Employee Commands

    /// <summary>
    /// Gets all employees in the database.
    /// </summary>
    /// <returns>IEnumerable of all employees</returns>
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IEnumerable<UserViewDTO>> GetAllViewModels()
    {
        var usersWithRoles = await (from users in _userManager.Users
            select new UserViewDTO()
            {
                Username = users.UserName,
                Email = users.Email,
                PhoneNumber = users.PhoneNumber,
                Role = (from ur in _context.UserRoles
                        join role in _context.Roles on ur.RoleId
                            equals role.Id
                            where ur.UserId == users.Id
                                select role.Name).ToList()
            }).ToListAsync();

        return usersWithRoles;
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
    /// <param name="role">User Role for the created user</param>
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
        else
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
            await _userManager.AddToRoleAsync(user, role);
        }
        return user;
    }
    #endregion

    #region RequestType Commands

    public async Task<RequestType> CreateRequestTypeAsync(RequestType requestType)
    {
        var found = await _context.RequestTypes.FirstOrDefaultAsync(r =>
            r.Name == requestType.Name);
        if (found != null) return null;

        var result = await _context.RequestTypes.AddAsync(requestType);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<RequestType> UpdateRequestTypeAsync(RequestType requestType)
    {
        var found = await _context.RequestTypes.FindAsync(requestType.RequestTypeID);
        if (found == null) return null;

        found.Name = requestType.Name;
        found.MaxDays = requestType.MaxDays;

        _context.RequestTypes.Update(found);
        await _context.SaveChangesAsync();
        return found;
    }

    public async Task<RequestType> DeleteRequestTypeAsync(int id)
    {
        var found = await _context.RequestTypes.FindAsync(id);
        if (found == null) return null;

        _context.RequestTypes.Remove(found);
        await _context.SaveChangesAsync();
        return found;
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