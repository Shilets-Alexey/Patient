using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsGenerator.Models
{
    public class PatientDto
    {
        public PatientName Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Active { get; set; }
    }
}
