using System;
using System.Security.Cryptography.X509Certificates;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Activities.Command;

public class UpdateAttendance
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }
    public class Handler(IUserAccessor userAccessor, AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var reactActivity = await context.ReactActivities
            .Include(x => x.Attendees).ThenInclude(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (reactActivity == null) return Result<Unit>.Failure("Activity not found", 404);
            var user = userAccessor.GetUserId();
            var attendance = reactActivity.Attendees.FirstOrDefault(x => x.UserId == user);
            var isHost = reactActivity.Attendees.Any(x => x.UserId == user && x.IsHost);
            if (attendance != null)
            {
                if (isHost) reactActivity.IsCancelled = !reactActivity.IsCancelled;
                else reactActivity.Attendees.Remove(attendance);
            }
            else
            {
                reactActivity.Attendees.Add(new ActivityAttendee
                {
                    UserId = user,
                    ActivityId = request.Id,
                    IsHost = false
                });
            }

            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to save activity", 400);
            
        }
    }
}
