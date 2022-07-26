using SampleAspNetCoreCQRS.Business.Entities;
using SampleAspNetCoreCQRS.Business.Models;
using SampleAspNetCoreCQRS.Business.Services;
using Moq;

namespace SampleAspNetCoreCQRS.Business.Tests.Mocks
{
    public static class MockPeopleService
    {
        public static Mock<IPeopleService> GetPeopleService()
        {
            var mockContext = new MockContext();

            var personEntities = new List<PersonEnt>
            {
                new PersonEnt
                {
                    Id = new Guid(mockContext.UserId),
                    Email = "benjamin.beck@benjaminbecktg.com",
                    Firstname = "Benjamin",
                    Lastname= "Beck",
                    Fullname = "Benjamin Beck",
                    CreatedOn = DateTime.UtcNow,
                },
                new PersonEnt
                {
                    Id = new Guid("BBBFDB2E-7A3B-41D0-80E1-8BF82BBCD051"),
                    Email = "john.doe@benjaminbecktg.com",
                    Firstname = "John",
                    Lastname= "Doe",
                    Fullname = "John Doe",
                    CreatedOn = DateTime.UtcNow
                }
            };

            var personModels = personEntities.Select(i => new Person
            {
                Email = i.Email,
                Firstname = i.Firstname,
                Fullname = i.Fullname,
                Id = i.Id,
                Lastname = i.Lastname,
                CreatedOn = i.CreatedOn,
                MobileNumber = i.MobileNumber,
            })
            .ToList();

            var primaryTestPerson = personEntities.FirstOrDefault(i => i.Id == new Guid(mockContext.UserId));
            var primaryTestPersonModel = personModels.FirstOrDefault(i => i.Id == primaryTestPerson.Id);

            var mockService = new Mock<IPeopleService>();

            mockService.Setup(r => r.GetPersonByIdAsync(primaryTestPerson.Id)).ReturnsAsync(primaryTestPerson);

            mockService.Setup(r => r.CreatePersonAsync(It.IsAny<PersonEnt>())).ReturnsAsync(true);

            mockService.Setup(r => r.UpdatePersonAsync(It.IsAny<PersonEnt>())).ReturnsAsync(true);

            mockService.Setup(r => r.GetPersonModelByIdAsync(primaryTestPersonModel.Id)).ReturnsAsync(primaryTestPersonModel);

            mockService.Setup(r => r.GetListPeopleModelByCriteriaAsync(It.IsAny<Criterias.PeopleCriteria>())).ReturnsAsync(personModels);

            return mockService;
        }
    }
}
