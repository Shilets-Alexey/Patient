using Microsoft.EntityFrameworkCore;
using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsApplication.DataAccess.Repositories
{
    public class PatientsRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public PatientsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Patient> GetPatients()
        {
            return _applicationDbContext.Patients.ToList();
        }

        public IEnumerable<Patient> GetPatients(Guid[] patientsId)
        {
            return _applicationDbContext.Patients.AsNoTracking().AsQueryable().Where(patient => patientsId.Contains(patient.Id)).AsEnumerable();
        }

        public Patient GetPatient(Guid patientId)
        {
            return _applicationDbContext.Patients.AsNoTracking().FirstOrDefault(patient => patient.Id == patientId);
        }

        public async Task<Guid> CreatePatient(Patient patient)
        {
            _applicationDbContext.Patients.Add(patient);
            await _applicationDbContext.SaveChangesAsync();
            return patient.Id;
        }

        public async Task<IEnumerable<Guid>> CreatePatients(IEnumerable<Patient> patients)
        {
            await _applicationDbContext.Patients.AddRangeAsync(patients);
            await _applicationDbContext.SaveChangesAsync();
            return patients.Select(patient => patient.Id);
        }

        public async Task<int> DeletePatient(Patient patient)
        {
            _applicationDbContext.Patients.Remove(patient);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> DeletePatients(IEnumerable<Patient> patients)
        {
            _applicationDbContext.Patients.RemoveRange(patients);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePatient(Patient patient)
        {
            _applicationDbContext.Patients.Update(patient);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePatients(IEnumerable<Patient> patients)
        {
            _applicationDbContext.Patients.UpdateRange(patients);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public int Count(IEnumerable<Guid> patientsId)
        {
            return _applicationDbContext.Patients.AsQueryable().Count(patient => patientsId.Contains(patient.Id));
        }

        public int Count()
        {
            return _applicationDbContext.Patients.Count();
        }
    }
}
