using System;
using System.Text.Json.Serialization;
using SampleAspNetCoreCQRS.Business.Models;
using SampleAspNetCoreCQRS.Business.Services;
using MediatR;

namespace SampleAspNetCoreCQRS.Business.Features.People.Queries
{
    public class GetPersonByIdQuery : IRequest<Person>
    {
        public Guid Id { get; set; }

        public class GetPersonByIdQueryHandler : BaseQueryHandler, IRequestHandler<GetPersonByIdQuery, Person>
        {
            private readonly IPeopleService _peopleService;

            public GetPersonByIdQueryHandler(IContext context, IPeopleService peopleService)
                : base(context)
            {
                _peopleService = peopleService;
            }

            public async Task<Person> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
            {
                if (query == null)
                {
                    return null;
                }

                return await _peopleService.GetPersonModelByIdAsync(query.Id);
            }
        }
    }
}
