using System.Collections.Generic;
using System.Threading.Tasks;
using BugTraq.Api.Commands;
using Microsoft.AspNetCore.Mvc;
using BugTraq.Api.Queries;
using MediatR;

namespace BugTraq.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BugsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBugs.Result>>> GetBugs()
        {
            return await _mediator.Send(new GetBugs.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBug.Result>> GetBug(int id)
        {
            var bug = await _mediator.Send(new GetBug.Query(id));
            return bug is null ? NotFound() : bug;
        }

        [HttpPut("UpdateStatus/{id}/{status}")]
        public async Task UpdateStatus(int id, string status)
        {
            await _mediator.Send(new UpdateBugStatus.Command {Id = id, Status = status});
        }

        [HttpPut("{id}")]
        public async Task PutBug([FromBody] UpdateBug.Command updateBug)
        {
            await _mediator.Send(updateBug);
        }

        [HttpPost]
        public async Task PostBug([FromBody] AddBug.Command bug)
        {
            await _mediator.Send(bug);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBug(int id)
        {
            await _mediator.Send(new DeleteBug.Command(id));
        }
    }
}
