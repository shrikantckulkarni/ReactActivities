using System;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.Activities.Queries;

public class GetReactActivityDetail
{
    public class Query : IRequest<Result<ReactActivityDto>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context,IMapper mapper) : IRequestHandler<Query, Result<ReactActivityDto>>
    {
        public async Task<Result<ReactActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var reactActivity = await context.ReactActivities.
            ProjectTo<ReactActivityDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => request.Id == x.Id, cancellationToken);

            if (reactActivity is null) return Result<ReactActivityDto>.Failure("Activity not found", 404);
            return Result<ReactActivityDto>.Success(reactActivity) ;
        }
    }
}
