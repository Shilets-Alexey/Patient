using System;
using System.ComponentModel.DataAnnotations;

namespace PatientsApplication.BusinessLogic.Models
{
    /// <summary>
    /// Patient name
    /// </summary>
    public class PatientName
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Use
        /// </summary>
        public string Use { get; set; }

        /// <summary>
        /// Family
        /// </summary>
        [Required()]
        public string Family { get; set; }

        /// <summary>
        /// Given
        /// </summary>
        public string[] Given { get; set; }
    }
}
