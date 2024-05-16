using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsGenerator.Models
{
    public class PatientName
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Use { get; set; }

        [Required()]
        public string Family { get; set; }

        public string[] Given { get; set; }
    }
}
