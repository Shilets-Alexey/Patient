using Microsoft.EntityFrameworkCore;
using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PatientsApplication.DataAccess.Repositories
{
    public class PatientsRepository : IPatiensRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public PatientsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Patient> Get()
        {
            return _applicationDbContext.Patients.ToList();
        }

        public IEnumerable<Patient> GetRange(Guid[] patientsId)
        {
            return _applicationDbContext.Patients.AsNoTracking().AsQueryable().Where(patient => patientsId.Contains(patient.Id)).AsEnumerable();
        }

        public Patient GetById(Guid patientId)
        {
            return _applicationDbContext.Patients.AsNoTracking().FirstOrDefault(patient => patient.Id == patientId);
        }

        public async Task<Guid> Create(Patient patient)
        {
            _applicationDbContext.Patients.Add(patient);
            await _applicationDbContext.SaveChangesAsync();
            return patient.Id;
        }

        public async Task<IEnumerable<Guid>> CreateRange(IEnumerable<Patient> patients)
        {
            await _applicationDbContext.Patients.AddRangeAsync(patients);
            await _applicationDbContext.SaveChangesAsync();
            return patients.Select(patient => patient.Id);
        }

        public async Task<int> Delete(Patient patient)
        {
            _applicationDbContext.Patients.Remove(patient);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRange(IEnumerable<Patient> patients)
        {
            _applicationDbContext.Patients.RemoveRange(patients);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> Update(Patient patient)
        {
            _applicationDbContext.Patients.Update(patient);
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateRange(IEnumerable<Patient> patients)
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

        public IEnumerable<Patient> GetByDate(Func<Patient, bool> filterMethod)
        {
            return _applicationDbContext.Patients.Where(filterMethod);
        }
    }
}