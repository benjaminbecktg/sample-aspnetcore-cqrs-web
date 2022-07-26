using SampleAspNetCoreCQRS.Business.Entities;
using SampleAspNetCoreCQRS.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleAspNetCoreCQRS.Business.Services
{
    public abstract class BaseService
    {
        public BaseService(IContext context)
        {
            Context = context;
            DataContext = Context.DataContext;
        }

        protected IContext Context { get; set; }
        protected IDataContext DataContext { get; set; }
    }
}
