using PatientsGenerator.Models;
using System;
using System.Collections.Generic;

namespace Generators
{
    class PatiensGenerator
    {
        public IEnumerable<PatientDto> Generate(int count)
        {
            List<PatientDto> patients = new List<PatientDto>();
            string[] genders = { "male", "female", "other", "unknown" };
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                PatientName patientName = new PatientName()
                {
                    Id = Guid.NewGuid(),
                    Family = Faker.Name.Last(),
                    Use = "official",
                    Given = GetGivenName(random)
                };
                PatientDto patient = new PatientDto() {
                    Name = patientName,
                    Active = Faker.Boolean.Random(),
                    BirthDate = DateTime.UtcNow.AddDays(-random.Next(2 * 365)),
                    Gender = genders[random.Next(0, 4)]
                };
                patients.Add(patient);
            }
            return patients;
        }

        private string[] GetGivenName(Random random)
        {
            int givenNameCount = random.Next(1, 5);
            string[] given = new string[givenNameCount];
            given[0] = Faker.Name.First();
            for(int i = 1; i < givenNameCount; i++)
            {
                given[i] = Faker.Name.Middle();
            }
            return given;
        }
    }
}