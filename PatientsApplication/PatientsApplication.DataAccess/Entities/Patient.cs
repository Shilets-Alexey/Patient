using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.DataAccess.Entities
{
    public class Patient : BaseEntity
    {
        public string Use { get; set; }

        [Required]
        public string Family { get; set; }

        public string Given { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        /*public Active Active { get; set; }

        public Gender Gender { get; set; }*/
    }
}
