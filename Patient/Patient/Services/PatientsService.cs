using PatientApplication.Models;
using PatientApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientApplication.Services
{
    public class PatientsService
    {
        private PatientsRepository _patientsRepository;

        public PatientsService(PatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        public ServiceResult<IEnumerable<PatientDto>> GetPatients()
        {
            return ServiceResult<IEnumerable<PatientDto>>.Create(_patientsRepository.GetPatients());
        }

        public async Task<ServiceResult<Guid>> CreatePatient(PatientDto patient)
        {
             return ServiceResult<Guid>.Create(await _patientsRepository.CreatePatient(patient));
        }
    }
}
