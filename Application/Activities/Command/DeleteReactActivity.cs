using System;
using System.Net;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistance;

namespace Application.Activities.Command;

public class DeleteReactActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command,Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var reactActivity = await context.ReactActivities
            .FindAsync([request.Id], cancellationToken);

            if (reactActivity == null) return Result<Unit>.Failure("Cannot find activity", 404);

            context.Remove(reactActivity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if(!result ) Result<Unit>.Failure("Failed to delete activity", 400);
            return Result<Unit>.Success(Unit.Value);
        }
    }

}
