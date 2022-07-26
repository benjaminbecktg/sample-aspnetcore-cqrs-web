using System.Text.Json.Serialization;
using SampleAspNetCoreCQRS.Business.Entities;
using SampleAspNetCoreCQRS.Business.Services;
using MediatR;

namespace SampleAspNetCoreCQRS.Business.Features.People.Commands
{
    public class CreatePersonCommand : IRequest<PersonEnt>
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Fullname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }

        public class CreatePersonCommandHandler : BaseCommandHandler, IRequestHandler<CreatePersonCommand, PersonEnt>
        {
            private readonly IPeopleService _peopleService;

            public CreatePersonCommandHandler(IContext context, IPeopleService peopleService)
                : base(context)
            {
                _peopleService = peopleService;
            }

            public async Task<PersonEnt> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
            {
                var entity = new PersonEnt
                {
                    Email = command.Email,
                    Firstname = command.Firstname,
                    Id = Guid.NewGuid(),
                    Lastname = command.Lastname,
                    Fullname = command.Fullname,
                    MobileNumber = command.MobileNumber,
                    CreatedOn = DateTime.UtcNow
                };

                await _peopleService.CreatePersonAsync(entity);
                
                return entity;
            }
        }
    }
}
