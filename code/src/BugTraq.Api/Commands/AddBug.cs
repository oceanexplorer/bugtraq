using System;
using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using FluentValidation;
using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BugTraq.Api.Commands
{
    public class AddBug
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(e => e.Title).NotEmpty();
                RuleFor(e => e.Description).NotEmpty();
                RuleFor(e => e.UserId).NotEmpty();
                RuleFor(e => e.Status).NotEmpty();
            }
        }
        
        public class Command : IRequest
        {
            public string Title { get; set;}
            public string Description { get; set;}
            public Guid UserId { get; set;}
            public string Status { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly BugTraqContext _context;

            public Handler(BugTraqContext context)
            {
                _context = context;
            }
            
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var bug = new Bug(request.Title, request.Description, request.Status, request.UserId);
                _context.Bugs.Add(bug);
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}