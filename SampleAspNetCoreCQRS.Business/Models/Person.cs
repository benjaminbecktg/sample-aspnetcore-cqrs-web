using System;

namespace SampleAspNetCoreCQRS.Business.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
