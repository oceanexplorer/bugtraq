using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTraq.Api.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTraq.Api.Models;
using BugTraq.Api.Queries;
using MediatR;

namespace BugTraq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly BugTraqContext _context;

        public BugsController(IMediator mediator, BugTraqContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBugs.Result>>> GetBugs()
        {
            return await _mediator.Send(new GetBugs.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);

            if (bug == null)
            {
                return NotFound();
            }

            return bug;
        }

        [HttpPut("UpdateStatus/{id}/{status}")]
        public async Task UpdateStatus(UpdateBugStatus.Command bugStatus)
        {
            await _mediator.Send(bugStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBug(int id, [FromForm]Bug bug)
        {
            if (id != bug.BugId)
            {
                return BadRequest();
            }

            _context.Entry(bug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task PostBug([FromBody] AddBug.Command bug)
        {
            await _mediator.Send(bug);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Bug>> DeleteBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();

            return bug;
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }
    }
}
