using System;
using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace BugTraq.Api.Queries
{
    public class GetBugValidator : AbstractValidator<GetBug.Query>
    {
        public GetBugValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
    
    public class GetBug
    {
        public class Query : IRequest<Result>
        {
            public int Id { get; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class Result
        {
            public int BugId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Status { get; set; }
            public Guid UserId { get; set; }
            public string UserFirstName { get; set; }
            public string UserSurname { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly BugTraqContext _context;
        
            public Handler(BugTraqContext context)
            {
                _context = context;
            }
            
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Bugs
                    .Where(b => b.BugId == request.Id)
                    .Select(b => new Result
                    {
                        BugId = b.BugId,
                        Title = b.Title,
                        Description = b.Description,
                        CreatedDate = b.CreatedDate,
                        Status = b.Status,
                        UserId = b.UserId,
                        UserFirstName = b.User.FirstName,
                        UserSurname = b.User.Surname
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}