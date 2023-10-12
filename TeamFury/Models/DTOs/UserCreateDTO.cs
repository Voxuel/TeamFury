using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

public class UserCreateDTO
{
    public string Username { get; set; }

    public string Password { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Role { get; set; }
}