using System;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.ReactActivities.Queries;

public class GetReactActivityList
{
    public class Query : IRequest<List<ReactActivity>> { }

    public  class Handler(AppDbContext context) : IRequestHandler<Query, List<ReactActivity>>
    {
        public async Task<List<ReactActivity>> Handle(Query request, CancellationToken cancellationToken)
        {
            return  await context.ReactActivities.ToListAsync(cancellationToken);
        }
    }
}
