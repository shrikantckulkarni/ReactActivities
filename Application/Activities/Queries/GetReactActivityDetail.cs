using System;
using Application.Core;
using Domain;
using MediatR;
using Persistance;

namespace Application.Activities.Queries;

public class GetReactActivityDetail
{
    public class Query : IRequest<Result<ReactActivity>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Result<ReactActivity>>
    {
        public async Task<Result<ReactActivity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var reactActivity = await context.ReactActivities.FindAsync([request.Id], cancellationToken);
            if (reactActivity is null) return Result<ReactActivity>.Failure("Activity not found", 404);
            return Result<ReactActivity>.Success(reactActivity) ;
        }
    }
}
