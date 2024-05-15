using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.DataAccess.Entities
{
    public abstract class BaseEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedOn { get; set; }
        
        public DateTime ModifiedOn { get; set; }
    }
}
