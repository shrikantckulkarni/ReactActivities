using System;
using System.Security.Claims;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Persistance;

namespace Infrastructure;

public class UserAccessor(IHttpContextAccessor httpContextAccessor,AppDbContext dbContext) : IUserAccessor
{
    public async Task<User> GetUserAsync()
    {
        var userId = GetUserId();
        return await dbContext.Users.FindAsync(userId) ?? throw new UnauthorizedAccessException("No user is logged on");
    }

    public string GetUserId()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("No user found");
    }
}
