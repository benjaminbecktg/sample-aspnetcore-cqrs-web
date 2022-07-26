using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAspNetCoreCQRS.Business.Tests
{
    public class MockContext : IContext
    {
        public MockContext()
        {
            OrganizationCode = "SAMPLE";
            UserId = "8C9688D1-A401-4CD7-8DDC-361E400C4C95";
            Username = "John.Doe";
        }

        public string OrganizationCode { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public IDataContext DataContext { get; set; }
    }
}
