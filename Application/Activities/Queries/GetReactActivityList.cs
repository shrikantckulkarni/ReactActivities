using System;
using Application.Activities.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.ReactActivities.Queries;

public class GetReactActivityList
{
    public class Query : IRequest<List<ReactActivityDto>> { }

    public  class Handler(AppDbContext context,IMapper mapper) : IRequestHandler<Query, List<ReactActivityDto>>
    {
        public async Task<List<ReactActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            return  await context.ReactActivities.
            ProjectTo<ReactActivityDto>(mapper.ConfigurationProvider).
            ToListAsync(cancellationToken);
        }
    }
}
