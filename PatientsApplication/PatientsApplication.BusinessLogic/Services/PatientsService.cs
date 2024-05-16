using AutoMapper;
using PatientsApplication.BusinessLogic.Helpers;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsApplication.BusinessLogic.Services
{
    public class PatientsService
    {
        private PatientsRepository _patientsRepository;

        private ActiveRepository _activeRepository;

        private GenderRepository _genderRepository;

        private readonly IEnumerable<Gender> _genders;

        private readonly IEnumerable<Active> _actives;

        private IMapper _mapper;

        public PatientsService(PatientsRepository patientsRepository,
                               ActiveRepository activeRepository,
                               GenderRepository genderRepository,
                               IMapper mapper)
        {
            _patientsRepository = patientsRepository;
            _activeRepository = activeRepository;
            _genderRepository = genderRepository;
            _mapper = mapper;
            _genders = _genderRepository.Get();
            _actives = _activeRepository.Get();
        }

        public ServiceResult<IEnumerable<PatientDto>> GetPatients()
        {
            return ServiceResult<IEnumerable<PatientDto>>.Create(_patientsRepository.GetPatients()
                                                         .Select(patient => _mapper.Map<PatientDto>(patient)));
        }

        public ServiceResult<IEnumerable<PatientDto>> GetPatients(Guid [] patientsId)
        {
            return ServiceResult<IEnumerable<PatientDto>>.Create(_patientsRepository.GetPatients(patientsId)
                                                         .Select(patient => _mapper.Map<PatientDto>(patient)));
        }

        public ServiceResult<PatientDto> GetPatient(Guid patientId)
        {
            return ServiceResult<PatientDto>.Create(_mapper.Map<PatientDto>(_patientsRepository.GetPatient(patientId)));
        }

        public async Task<ServiceResult<Guid>> CreatePatient(PatientDto patientDto)
        {
            return ServiceResult<Guid>.Create(await _patientsRepository.CreatePatient(_mapper.Map<PatientDto, Patient>(patientDto,
                                                                                                                       opt => PatientsHelper.AfterMap(opt, _genders, _actives))));
        }
        public async Task<ServiceResult<IEnumerable<Guid>>> CreatePatients(PatientDto[] patients)
        {
            return ServiceResult<IEnumerable<Guid>>.Create(await _patientsRepository.CreatePatients(patients.Select(patientDto => _mapper.Map<PatientDto, Patient>(patientDto,
                                                                                                                                                                   opt => PatientsHelper.AfterMap(opt, _genders, _actives)))));
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
            ValidationResult validationInfo = ValidationHelper.ValidatePatients(patients.Count(), patientsId.Length);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            return ServiceResult<int>.Create(await _patientsRepository.DeletePatients(patients));
        }

        public async Task<ServiceResult<int>> UpdatePatient(PatientDto patientDto)
        {
            Patient existingPatient = _patientsRepository.GetPatient(patientDto.Name.Id);
            ValidationResult validationInfo = ValidationHelper.ValidatePatient(existingPatient);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            Patient patient = _mapper.Map<PatientDto, Patient>(patientDto,
                                                               opt => PatientsHelper.AfterMap(opt, _genders, _actives, existingPatient.CreatedOn));
            return ServiceResult<int>.Create(await _patientsRepository.UpdatePatient(patient));
        }

        public async Task<ServiceResult<int>> UpdatePatients(PatientDto[] patientsDto)
        {
            IEnumerable<Patient> existingPatients = _patientsRepository.GetPatients(patientsDto.Select(patient => patient.Name.Id).ToArray());
            ValidationResult validationInfo = ValidationHelper.ValidatePatients(existingPatients.Count(), patientsDto.Length);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            IEnumerable<Patient> patients = patientsDto.Select(patientDto => {
                Patient existPatient = existingPatients.FirstOrDefault(existingPatient => existingPatient.Id == patientDto.Name.Id);
                return _mapper.Map<PatientDto, Patient>(patientDto,
                                                opt => PatientsHelper.AfterMap(opt, _genders, _actives, existPatient.CreatedOn));
            });
            return ServiceResult<int>.Create(await _patientsRepository.UpdatePatients(patients));
        }
    }
}
