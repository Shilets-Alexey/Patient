using AutoMapper;
using PatientsApplication.BusinessLogic.Helpers;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Repositories;

namespace PatientsApplication.BusinessLogic.Services
{
    public class PatientsService
    {
        private PatientsRepository _patientsRepository;

        private IMapper _mapper;

        public PatientsService(PatientsRepository patientsRepository, IMapper mapper)
        {
            _patientsRepository = patientsRepository;
            _mapper = mapper;
        }

        public ServiceResult<IEnumerable<PatientDto>> GetPatients()
        {
            return ServiceResult<IEnumerable<PatientDto>>.Create(_patientsRepository.GetPatients().Select(patient => _mapper.Map<PatientDto>(patient)));
        }

        public ServiceResult<IEnumerable<PatientDto>> GetPatients(Guid [] patientsId)
        {
            return ServiceResult<IEnumerable<PatientDto>>.Create(_patientsRepository.GetPatients(patientsId).Select(patient => _mapper.Map<PatientDto>(patient)));
        }

        public ServiceResult<PatientDto> GetPatient(Guid patientId)
        {
            return ServiceResult<PatientDto>.Create(_mapper.Map<PatientDto>(_patientsRepository.GetPatient(patientId)));
        }

        public async Task<ServiceResult<Guid>> CreatePatient(PatientDto patient)
        {
            return ServiceResult<Guid>.Create(await _patientsRepository.CreatePatient(_mapper.Map<Patient>(patient)));
        }

        public async Task<ServiceResult<IEnumerable<Guid>>> CreatePatients(PatientDto[] patients)
        {
            return ServiceResult<IEnumerable<Guid>>.Create(await _patientsRepository.CreatePatients(patients.Select(patientDto => _mapper.Map<Patient>(patientDto))));
        }

        public async Task<ServiceResult<int>> DeletePatient(Guid patientId)
        {
            Patient patient = _patientsRepository.GetPatient(patientId);
            ValidationResult validationInfo = ValidationHelper.ValidatePatient(patient);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            return ServiceResult<int>.Create(await _patientsRepository.DeletePatient(patient));
        }

        public async Task<ServiceResult<int>> DeletePatients(Guid[] patientsId)
        {
            IEnumerable<Patient> patients = _patientsRepository.GetPatients(patientsId);
            ValidationResult validationInfo = ValidationHelper.ValidatePatients(patients, patientsId.Length);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            return ServiceResult<int>.Create(await _patientsRepository.DeletePatients(patients));
        }

        public async Task<ServiceResult<int>> UpdatePatient(PatientDto patientDto)
        {
            Patient patient = _mapper.Map<Patient>(patientDto);
            ValidationResult validationInfo = ValidationHelper.ValidatePatient(patient);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            return ServiceResult<int>.Create(await _patientsRepository.UpdatePatient(patient));
        }

        public async Task<ServiceResult<int>> UpdatePatients(PatientDto[] patientsDto)
        {
            IEnumerable<Patient> existingPatients = _patientsRepository.GetPatients(patientsDto.Select(patient => patient.Name.Id).ToArray());
            ValidationResult validationInfo = ValidationHelper.ValidatePatients(existingPatients, patientsDto.Length);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            IEnumerable<Patient> patients = patientsDto.Select(patientDto => _mapper.Map<Patient>(patientDto));
            return ServiceResult<int>.Create(await _patientsRepository.UpdatePatients(patients));
        }
    }
}
