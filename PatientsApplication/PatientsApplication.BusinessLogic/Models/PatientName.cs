﻿using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.BusinessLogic.Models
{
    public class PatientName
    {
        public Guid Id { get; set; }

        public string Use { get; set; }

        [Required()]
        public string Family { get; set; }

        public string[] Given { get; set; }
    }
}
