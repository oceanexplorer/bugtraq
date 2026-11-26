using System.Threading;
using System.Threading.Tasks;
using BugTraq.Api.Models;
using FluentValidation;
using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BugTraq.Api.Commands
{
    public class DeleteBug
    {
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();    
            }
        }
        
        public class Command : IRequest
        {
            public int Id { get; }

            public Command(int id)
            {
                Id = id;
            }
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
                var bug = await _context.Bugs.FindAsync([request.Id], cancellationToken);

                if (bug != null)
                {
                    _context.Bugs.Remove(bug);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}