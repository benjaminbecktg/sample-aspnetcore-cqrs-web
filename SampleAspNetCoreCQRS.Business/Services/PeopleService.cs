using SampleAspNetCoreCQRS.Business.Criterias;
using SampleAspNetCoreCQRS.Business.Entities;
using SampleAspNetCoreCQRS.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleAspNetCoreCQRS.Business.Services
{
    public partial interface IPeopleService
    {
        Task<PersonEnt> GetPersonByIdAsync(Guid id);
        Task<bool> CreatePersonAsync(PersonEnt entity);
        Task<bool> UpdatePersonAsync(PersonEnt entity);

        Task<Person> GetPersonModelByIdAsync(Guid id);
        Task<IList<Person>> GetListPeopleModelByCriteriaAsync(PeopleCriteria criteria);
    }

    public partial class PeopleService : BaseService, IPeopleService
    {
        public PeopleService(IContext context)
            : base(context)
        {

        }
        public async Task<PersonEnt> GetPersonByIdAsync(Guid id)
        {
            return await DataContext.People
                .Where(i => !i.IsDeleted
                    && i.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreatePersonAsync(PersonEnt entity)
        {
            entity.CreatedOn = DateTime.UtcNow;

            DataContext.Add(entity);
            return await DataContext.CommitAsync();
        }

        public async Task<bool> UpdatePersonAsync(PersonEnt entity)
        {
            entity.Firstname = entity.Firstname;
            entity.Fullname = entity.Fullname;
            entity.Lastname = entity.Lastname;
            entity.MobileNumber = entity.MobileNumber;

            DataContext.Update(entity);
            return await DataContext.CommitAsync();
        }

        public async Task<Person> GetPersonModelByIdAsync(Guid id)
        {
            return await DataContext.People.AsNoTracking()
                .Where(i => !i.IsDeleted && i.Id == id)
                .Select(i => new Person
                {
                    Email = i.Email,
                    CreatedOn = i.CreatedOn,
                    Firstname = i.Firstname,
                    Fullname = i.Fullname,
                    Id = i.Id,
                    Lastname = i.Lastname,
                    MobileNumber = i.MobileNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IList<Person>> GetListPeopleModelByCriteriaAsync(PeopleCriteria criteria)
        {
            var query = DataContext.People.AsNoTracking()
                .Where(i => !i.IsDeleted);

            if (!string.IsNullOrEmpty(criteria.SearchText))
            {
                criteria.SearchText = criteria.SearchText.ToLower();

                query = query.Where(i =>
                       i.Fullname.ToLower().Contains(criteria.SearchText)
                    && i.Firstname.ToLower().Contains(criteria.SearchText)
                    && i.Lastname.ToLower().Contains(criteria.SearchText)
                    && i.MobileNumber.ToLower().Contains(criteria.SearchText));
            }

            return await query.Select(i => new Person
                {
                    Email = i.Email,
                    CreatedOn = i.CreatedOn,
                    Firstname = i.Firstname,
                    Fullname = i.Fullname,
                    Id = i.Id,
                    Lastname = i.Lastname,
                    MobileNumber = i.MobileNumber
                })
                .ToListAsync();
        }
    }
}
