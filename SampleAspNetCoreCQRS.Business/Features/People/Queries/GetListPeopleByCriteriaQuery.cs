using SampleAspNetCoreCQRS.Business.Criterias;
using SampleAspNetCoreCQRS.Business.Models;
using SampleAspNetCoreCQRS.Business.Services;
using MediatR;

namespace SampleAspNetCoreCQRS.Business.Features.People.Queries
{
    public class GetListPeopleByCriteriaQuery : IRequest<IList<Person>>
    {
        public PeopleCriteria Criteria { get; set; }

        public class GetListPeopleByCriteriaQueryHandler : BaseQueryHandler, IRequestHandler<GetListPeopleByCriteriaQuery, IList<Person>>
        {
            private readonly IPeopleService _peopleService;

            public GetListPeopleByCriteriaQueryHandler(IContext context, IPeopleService userService)
                :base(context)
            {
                _peopleService = userService;
            }

            public async Task<IList<Person>> Handle(GetListPeopleByCriteriaQuery query, CancellationToken cancellationToken)
            {
                return await _peopleService.GetListPeopleModelByCriteriaAsync(query.Criteria);
            }
        }
    }
}
