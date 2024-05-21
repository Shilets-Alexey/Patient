using PatientsApplication.BusinessLogic.Fhir;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PatientsApplication.BusinessLogic.Helpers
{
    public class ValidationHelper
    {
        public static ValidationResult ValidatePatients(int patientsCount, int patientsDtoCount)
        {
            if (patientsCount != patientsDtoCount)
            {
                return ValidationResult.ValidationProblem(new Dictionary<string, string[]>() { { "patients", new string[] { "Not all ids contained in the request have records" } } });
            }
            return new ValidationResult();
        }

        public static ValidationResult ValidatePatient(Patient patient)
        {
            if (patient == default(Patient))
            {
                return ValidationResult.ValidationProblem(new Dictionary<string, string[]>() { { "patient", new string[] { "There is no record for the specified id" } } });
            }
            return new ValidationResult();
        }

        public static ValidationResult ValidateFhirDate(FhirDate fhirDate) {
            if (fhirDate == default(FhirDate))
            {
                return ValidationResult.ValidationProblem(new Dictionary<string, string[]>() { { "date", new string[] { "invalid date" } } });
            }
            if(!fhirDate.Success)
            {
                return ValidationResult.ValidationProblem(new Dictionary<string, string[]>() { { "date", new string[] { fhirDate.ErrorMessage } } });
            }
            return new ValidationResult();
        }
    }
}
