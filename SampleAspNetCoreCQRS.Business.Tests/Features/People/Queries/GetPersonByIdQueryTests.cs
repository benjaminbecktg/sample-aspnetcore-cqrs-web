using SampleAspNetCoreCQRS.Business.Features.People.Queries;
using SampleAspNetCoreCQRS.Business.Services;
using SampleAspNetCoreCQRS.Business.Tests.Mocks;
using Moq;
using Xunit;

namespace SampleAspNetCoreCQRS.Business.Tests.Features.People.Queries
{
    public class GetPersonByIdQueryTests
    {
        private readonly Mock<IPeopleService> _mockPeopleService;
        private readonly IContext _mockContext;

        public GetPersonByIdQueryTests()
        {
            _mockContext = new MockContext();
            _mockPeopleService = MockPeopleService.GetPeopleService();
        }

        [Fact]
        public async Task GetPersonByIdQueryTests_Success()
        {
            var handler = new GetPersonByIdQuery.GetPersonByIdQueryHandler(_mockContext, _mockPeopleService.Object);

            var payload = new GetPersonByIdQuery 
            { 
                Id = new Guid(_mockContext.UserId)
            };

            var result = await handler.Handle(payload, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(payload.Id, result.Id);
        }
    }
}
