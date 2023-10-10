﻿using Models.DTOs;

namespace TeamFury_API.Services.SecurityServices;

public interface IAuthService
{
    Task<(int, string)> Registration();
    Task<(int, string)> Login(LoginDTO login);
}