using AutoMapper;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Context;
using PatientsApplication.DataAccess.Entities;

namespace PatientsApplication.BusinessLogic.Repositories
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
