using SampleAspNetCoreCQRS.Business.Features.People.Commands;
using SampleAspNetCoreCQRS.Business.Services;
using SampleAspNetCoreCQRS.Business.Tests.Mocks;
using Moq;
using Xunit;

namespace SampleAspNetCoreCQRS.Business.Tests.Features.People.Commands
{
    public class CreatePersonCommandTests
    {
        private readonly Mock<IPeopleService> _mockPeopleService;
        private readonly IContext _mockContext;

        public CreatePersonCommandTests()
        {
            _mockContext = new MockContext();
            _mockPeopleService = MockPeopleService.GetPeopleService();

        }

        [Fact]
        public async Task CreatePersonCommandTests_Success()
        {
            var handler = new CreatePersonCommand.CreatePersonCommandHandler(_mockContext, _mockPeopleService.Object);

            var payload = new CreatePersonCommand
            { 
                Email = "benjamin.beck@benjamibecktg.com",
                Firstname = "Benjamin",
                Lastname = "Beck",
                Fullname = "Benjamin Beck"
            };

            var result = await handler.Handle(payload, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(payload.Email, result.Email);
            Assert.Equal(payload.Firstname, result.Firstname);
            Assert.Equal(payload.Lastname, result.Lastname);
            Assert.Equal(payload.Fullname, result.Fullname);
            Assert.Equal(payload.MobileNumber, result.MobileNumber);
        }
    }
}
