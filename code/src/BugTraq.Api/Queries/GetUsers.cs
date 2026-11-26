using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace BugTraq.Api.Queries
{
    public class GetUsers
    {
        public class Query : IRequest<List<Result>>
        {
        }

        public class Result
        {
            public Guid UserId { get; set; }
            public string FirstName { get; set; }
            public string Surname { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Result>>
        {
            private readonly BugTraqContext _context;
        
            public Handler(BugTraqContext context)
            {
                _context = context;
            }
            
            public async Task<List<Result>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Users
                    .Select(u => new Result { UserId = u.UserId, FirstName = u.FirstName, Surname = u.Surname })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}