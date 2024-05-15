using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Repositories;

namespace PatientsApplication.BusinessLogic.Helpers
{
    public class ValidationHelper
    {
        public static ValidationResult ValidatePatients(IEnumerable<Patient> patients, int inputPatientsCount)
        {
            if(patients.Count() < inputPatientsCount)
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
    }
}
