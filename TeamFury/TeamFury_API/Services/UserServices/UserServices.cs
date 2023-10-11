using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.UserServices;

public class UserServices : IUserServices
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UserServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<User> CreateEmployeeUser(UserCreateDTO user_c_dto)
    {
        var userFound = await _userManager.FindByNameAsync(user_c_dto.Username);

        if (userFound != null) return null;
        
        var user = _mapper.Map<User>(user_c_dto);
        
        var createUserResult = await _userManager.CreateAsync(user, user_c_dto.Password);

        if (!createUserResult.Succeeded) return null;
        if (await _roleManager.RoleExistsAsync("Employee"))
        {
            await _userManager.AddToRoleAsync(user, "Employee");
        }

        return user;
    }
    
    
    // Save for azure db migration.
    public async Task CreateRoleAsync()
    {
        await _roleManager.CreateAsync(new IdentityRole("Employee"));
    }
}