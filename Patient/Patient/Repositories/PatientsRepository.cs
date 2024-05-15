using AutoMapper;
using PatientApplication.Models;
using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientApplication.Repositories
{
    public class PatientsRepository
    {
        private ApplicationDbContext _applicationDbContext;
        private IMapper _mapper;

        public PatientsRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public IEnumerable<PatientDto> GetPatients()
        {
            return _applicationDbContext.Patients.Select(patient => _mapper.Map<PatientDto>(patient));
        }

        public async Task<Guid> CreatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            _applicationDbContext.Patients.Add(patient);
            await _applicationDbContext.SaveChangesAsync();
            return patient.Id;
        }
    }
}
