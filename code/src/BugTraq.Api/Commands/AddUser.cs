using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using FluentValidation;
using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BugTraq.Api.Commands
{
    public class AddUser
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(e => e.FirstName).NotEmpty();
                RuleFor(e => e.Surname).NotEmpty();
            }
        }
        
        public class Command : IRequest
        {
            public string FirstName { get; set;}
            public string Surname { get; set;}
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
                var user = new User(request.FirstName, request.Surname);
                _context.Users.Add(user);
                
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}