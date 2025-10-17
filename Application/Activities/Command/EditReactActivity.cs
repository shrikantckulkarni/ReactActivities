using System;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistance;

namespace Application.Activities.Command;

public class EditReactActivity
{
  public class Command : IRequest<Result<ReactActivity>>
    {
        public required EditReactActivityDto ReactActivityDto { get; set; }
    }

    public class Handler(AppDbContext context,IMapper mapper) : IRequestHandler<Command,Result<ReactActivity>>
    {
        public async Task<Result<ReactActivity>> Handle(Command request, CancellationToken cancellationToken)
        {
            var reactActivity = await context.ReactActivities.FindAsync([request.ReactActivityDto.Id], cancellationToken);
    if (reactActivity is null) return Result<ReactActivity>.Failure("Activity not found", 404);               
            mapper.Map(request.ReactActivityDto, reactActivity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if(!result ) Result<Unit>.Failure("Failed to update activity", 400);
            return Result<ReactActivity>.Success(reactActivity);
        }
    }
}
