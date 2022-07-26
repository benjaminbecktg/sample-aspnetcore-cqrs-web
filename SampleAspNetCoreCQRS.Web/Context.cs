using System.Security.Claims;
using SampleAspNetCoreCQRS.Business;

namespace SampleAspNetCoreCQRS.Web
{
    public interface IContext : Business.IContext
    {

    }

    public class Context : IContext
    {
        public Context(IDataContext dataContext) 
        {
            DataContext = dataContext;
        }

        public string OrganizationCode { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }

        public IDataContext DataContext { get; set; }
    }
}
