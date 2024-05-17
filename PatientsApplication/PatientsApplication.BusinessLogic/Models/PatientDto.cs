using PatientsApplication.DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.BusinessLogic.Models
{
    /// <summary>
    /// Patient
    /// </summary>
    public class PatientDto
    {
        /// <summary>
        /// Name
        /// </summary>
        public PatientName Name { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// BirthDate
        /// </summary>
        [Required()]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        public bool Active { get; set; }
    }
}
