using Microsoft.AspNetCore.Identity;
using Models.Models;

namespace Models.DTOs;

public class RequestWithUser
{
    public Request Request { get; set; }

    public string UserId { get; set; }
    public string UserName { get; set; }
}