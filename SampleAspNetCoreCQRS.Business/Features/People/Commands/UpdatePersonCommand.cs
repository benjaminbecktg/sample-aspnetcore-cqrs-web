using System.Text.Json.Serialization;
using SampleAspNetCoreCQRS.Business.Entities;
using SampleAspNetCoreCQRS.Business.Services;
using MediatR;

namespace SampleAspNetCoreCQRS.Business.Features.People.Commands
{
    public class UpdatePersonCommand : IRequest<PersonEnt>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Fullname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }

        public class UpdatePersonCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonCommand, PersonEnt>
        {
            private readonly IPeopleService _peopleService;

            public UpdatePersonCommandHandler(IContext context, IPeopleService peopleService)
                : base(context)
            {
                _peopleService = peopleService;
            }

            public async Task<PersonEnt> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
            {
                var entity = await _peopleService.GetPersonByIdAsync(command.Id);
                if (entity != null)
                {
                    entity.Email = command.Email;
                    entity.Firstname = command.Firstname;
                    entity.Fullname = command.Fullname;
                    entity.Lastname = command.Lastname;
                    entity.MobileNumber = command.MobileNumber;

                    await _peopleService.UpdatePersonAsync(entity);
                }

                return entity;
            }
        }
    }
}
