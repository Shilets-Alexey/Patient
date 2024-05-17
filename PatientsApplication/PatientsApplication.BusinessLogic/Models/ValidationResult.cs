using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PatientsApplication.BusinessLogic.Models
{
    /// <summary>
    /// Validation Result
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Error list
        /// </summary>
        [JsonPropertyName("errors")]
        public Dictionary<string, string[]> Errors { get; private set; }

        /// <summary>
        /// Validation flag
        /// </summary>
        [JsonPropertyName("isValid")]
        public bool IsValid { get; private set; }

        public ValidationResult()
        {
            Errors = new Dictionary<string, string[]>();
            IsValid = true;
        }

        public static ValidationResult ValidationProblem(Dictionary<string, string[]> errors) => new ValidationResult() { Errors = errors, IsValid = false };
    }
}
