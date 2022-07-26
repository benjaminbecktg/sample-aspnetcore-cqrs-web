using System;
using Microsoft.EntityFrameworkCore;

namespace SampleAspNetCoreCQRS.Business.Features
{
    public abstract class BaseQueryHandler
    {
        protected readonly IContext Context;

        public BaseQueryHandler(IContext context)
        {
            Context = context;
        }


        protected Task<List<T>> GetStandardLinqCriteriaListAsync<T>(IQueryable<T> instance, Criterias.BaseCriteria criteria)
            where T : class
        {
            // ToDo: Page Filtering logic using criteria object

            var query = instance;

            return query.ToListAsync();
        }

    }
}
