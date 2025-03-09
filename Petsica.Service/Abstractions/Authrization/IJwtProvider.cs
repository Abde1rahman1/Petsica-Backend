﻿namespace Petsica.Service.Services.Authrization
{
    public interface IJwtProvider
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
