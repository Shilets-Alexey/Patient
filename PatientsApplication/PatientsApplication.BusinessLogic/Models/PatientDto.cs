using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.BusinessLogic.Models
{
    public class PatientDto
    {
        public PatientName Name { get; set; }

        public string Gender { get; set; }

        [Required()]
        public DateTime BirthDate { get; set; }

        public string Active { get; set; }
    }
}
