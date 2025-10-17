using System;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistance;

namespace Application.Activities.Command;

public class CreateReactActivity
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateReactActivityDto ReactActivityDto { get; set; }
    }

    public class Handler(AppDbContext context,IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var reactActivity = mapper.Map<ReactActivity>(request.ReactActivityDto);
             context.ReactActivities.Add(reactActivity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if (!result) Result<Unit>.Failure("Failed to create activity", 400);
            return Result<string>.Success(reactActivity.Id);
        }
    }
}
