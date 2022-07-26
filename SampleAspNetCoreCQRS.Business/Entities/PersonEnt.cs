using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleAspNetCoreCQRS.Business.Entities
{
    [Table("People")]
    public partial class PersonEnt
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        [StringLength(200)]
        public string Fullname { get; set; }

        [StringLength(30)]
        public string MobileNumber { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
