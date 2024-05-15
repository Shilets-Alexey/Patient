using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.DataAccess.Entities
{
    public abstract class BaseLookup : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
