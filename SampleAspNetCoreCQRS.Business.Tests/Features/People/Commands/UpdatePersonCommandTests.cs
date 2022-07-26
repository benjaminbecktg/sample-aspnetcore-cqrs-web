using SampleAspNetCoreCQRS.Business.Features.People.Commands;
using SampleAspNetCoreCQRS.Business.Services;
using SampleAspNetCoreCQRS.Business.Tests.Mocks;
using Moq;
using Xunit;

namespace SampleAspNetCoreCQRS.Business.Tests.Features.People.Commands
{
    public class UpdatePersonCommandTests
    {
        private readonly Mock<IPeopleService> _mockPeopleService;
        private readonly IContext _mockContext;

        public UpdatePersonCommandTests()
        {
            _mockContext = new MockContext();
            _mockPeopleService = MockPeopleService.GetPeopleService();

        }

        [Fact]
        public async Task UpdatePersonCommandTests_Success()
        {
            var handler = new UpdatePersonCommand.UpdatePersonCommandHandler(_mockContext, _mockPeopleService.Object);

            var payload = new UpdatePersonCommand
            { 
                Email = "me@brucewayne.com",
                Firstname = "Bruce",
                Lastname = "Wayne",
                Fullname = "Bruce Wayne",
                Id = new Guid(_mockContext.UserId)
            };

            var result = await handler.Handle(payload, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(payload.Email, result.Email);
            Assert.Equal(payload.Firstname, result.Firstname);
            Assert.Equal(payload.Fullname, result.Fullname);
            Assert.Equal(payload.Lastname, result.Lastname);
            Assert.Equal(payload.MobileNumber, result.MobileNumber);
        }
    }
}
