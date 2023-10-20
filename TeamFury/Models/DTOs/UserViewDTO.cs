using Microsoft.AspNetCore.Identity;

namespace Models.DTOs;

public class UserViewDTO
{
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public List<string> Role { get; set; }
}