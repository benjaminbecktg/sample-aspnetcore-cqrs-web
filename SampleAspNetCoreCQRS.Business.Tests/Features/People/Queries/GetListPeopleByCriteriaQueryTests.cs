using SampleAspNetCoreCQRS.Business.Features.People.Queries;
using SampleAspNetCoreCQRS.Business.Services;
using SampleAspNetCoreCQRS.Business.Tests.Mocks;
using Moq;
using Xunit;

namespace SampleAspNetCoreCQRS.Business.Tests.Features.People.Queries
{
    public class GetListPeopleByCriteriaQueryTests
    {
        private readonly Mock<IPeopleService> _mockPeopleService;
        private readonly IContext _mockContext;

        public GetListPeopleByCriteriaQueryTests()
        {
            _mockContext = new MockContext();
            _mockPeopleService = MockPeopleService.GetPeopleService();
        }

        [Fact]
        public async Task GetListPeopleByCriteriaQueryTests_Success()
        {
            var handler = new GetListPeopleByCriteriaQuery.GetListPeopleByCriteriaQueryHandler(_mockContext, _mockPeopleService.Object);

            var payload = new GetListPeopleByCriteriaQuery
            { 
                Criteria = new Criterias.PeopleCriteria
                {
                    CurrentPage = 1,
                    PageSize = 10,
                }
            };

            var result = await handler.Handle(payload, CancellationToken.None);

            Assert.NotNull(result);
        }
    }
}
