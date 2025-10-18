using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Persistance;


namespace Infrastructure.Security;

public class IHostRequirement : IAuthorizationRequirement
{

}

public class IsHostRequirementHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<IHostRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IHostRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return;
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext?.GetRouteValue("id") is not string activityId) return;
        var attendee = await dbContext.ActivityAttendees.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.ActivityId == activityId);
        if (attendee == null) return;
        if (attendee.IsHost) context.Succeed(requirement);


    }
}
