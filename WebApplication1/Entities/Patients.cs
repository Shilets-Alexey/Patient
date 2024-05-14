using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.DataAccess.Entities
{
    public class Patients : BaseEntity
    {
        public string Use { get; set; }

        [Required]
        public string Family { get; set; }

        public string Given { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public Active Active { get; set; }

        public Genders Gender { get; set; }
    }
}
