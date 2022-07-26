using SampleAspNetCoreCQRS.Business.Criterias;
using SampleAspNetCoreCQRS.Business.Features.People.Commands;
using SampleAspNetCoreCQRS.Business.Features.People.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SampleAspNetCoreCQRS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : BaseApiController
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IMediator _mediator;

        public PeopleController(ILogger<PeopleController> logger, IContext context, IMediator mediator)
            : base(context)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetListPeopleByCriteriaAsync([FromQuery] PeopleCriteria criteria)
        {
            return await ExecuteActionAsync(async () =>
            {
                criteria = criteria ?? new PeopleCriteria();
                
                return await _mediator.Send(new GetListPeopleByCriteriaQuery { Criteria = criteria });
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPersonByIdAsync(Guid id)
        {
            return await ExecuteActionAsync(async () =>
            {
                return await _mediator.Send(new GetPersonByIdQuery { Id = id });
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CreatePersonCommand command)
        {
            return await ExecuteActionAsync(async () =>
            {
                // Create the record
                var personEntity = await _mediator.Send(command);

                // Fetch the record
                return await _mediator.Send(new GetPersonByIdQuery { Id = personEntity.Id });
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePersonAsync(Guid id, [FromBody] UpdatePersonCommand command)
        {
            return await ExecuteActionAsync(async () =>
            {
                // Update the record
                command.Id = id;
                var personEntity = await _mediator.Send(command);

                // Fetch the record
                return await _mediator.Send(new GetPersonByIdQuery { Id = personEntity.Id });
            });
        }
    }
}
