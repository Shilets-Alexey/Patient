using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.DataAccess.Entities
{
    public class BaseLookup : BaseEntity
    {
        [Required]
        public string Name {  get; set; }
    }
}
