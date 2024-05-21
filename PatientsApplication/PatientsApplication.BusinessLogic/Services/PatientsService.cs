using AutoMapper;
using PatientsApplication.BusinessLogic.Fhir;
using PatientsApplication.BusinessLogic.Helpers;
using PatientsApplication.BusinessLogic.Interfaces;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using PatientsApplication.DataAccess.Interfaces;
using PatientsApplication.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsApplication.BusinessLogic.Services
{
    public class PatientsService<T> : IEntityService<T> where T : PatientDto
    {
        //private IEntityRepository<Patient> _patientsRepository;

        private IPatiensRepository _patientsRepository;

        private ILookupRepository _lookupRepository;

        private readonly IEnumerable<Gender> _genders;

        private readonly IEnumerable<Active> _actives;

        private IMapper _mapper;

        public PatientsService(IPatiensRepository patientsRepository,
                               ILookupRepository lookupRepository,
                               IMapper mapper)
        {
            _patientsRepository = patientsRepository;
            _lookupRepository = lookupRepository;
            _mapper = mapper;
            _genders = _lookupRepository.Get<Gender>();
            _actives = _lookupRepository.Get<Active>();
        }

        public ServiceResult<IEnumerable<T>> Get()
        {
            return ServiceResult<IEnumerable<T>>.Create(_patientsRepository.Get()
                                                         .Select(patient => _mapper.Map<T>(patient)));
        }

        public ServiceResult<IEnumerable<T>> GetRange(Guid [] patientsId)
        {
            return ServiceResult<IEnumerable<T>>.Create(_patientsRepository.GetRange(patientsId)
                                                         .Select(patient => _mapper.Map<T>(patient)));
        }

        public ServiceResult<T> GetById(Guid patientId)
        {
            return ServiceResult<T>.Create(_mapper.Map<T>(_patientsRepository.GetById(patientId)));
        }

        public async Task<ServiceResult<Guid>> Create(T patientDto)
        {
            return ServiceResult<Guid>.Create(await _patientsRepository.Create(_mapper.Map<PatientDto, Patient>(patientDto,
                                                                                                                opt => PatientsHelper.AfterMap(opt, _genders, _actives))));
        }
        public async Task<ServiceResult<IEnumerable<Guid>>> CreateRange(T[] patients)
        {
            return ServiceResult<IEnumerable<Guid>>.Create(await _patientsRepository.CreateRange(patients.Select(patientDto => _mapper.Map<PatientDto, Patient>(patientDto,
                                                                                                                                                                   opt => PatientsHelper.AfterMap(opt, _genders, _actives)))));
        }

        public async Task<ServiceResult<int>> Delete(Guid patientId)
        {
            Patient patient = _patientsRepository.GetById(patientId);
            ValidationResult validationInfo = ValidationHelper.ValidatePatient(patient);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            return ServiceResult<int>.Create(await _patientsRepository.Delete(patient));
        }

        public async Task<ServiceResult<int>> DeleteRange(Guid[] patientsId)
        {
            IEnumerable<Patient> patients = _patientsRepository.GetRange(patientsId);
            ValidationResult validationInfo = ValidationHelper.ValidatePatients(patients.Count(), patientsId.Length);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            return ServiceResult<int>.Create(await _patientsRepository.DeleteRange(patients));
        }

        public async Task<ServiceResult<int>> Update(T patientDto)
        {
            Patient existingPatient = _patientsRepository.GetById(patientDto.Name.Id);
            ValidationResult validationInfo = ValidationHelper.ValidatePatient(existingPatient);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<int>.Create(validationInfo);
            }
            Patient patient = _mapper.Map<PatientDto, Patient>(patientDto,
                                                               opt => PatientsHelper.AfterMap(opt, _genders, _actives, existingPatient.CreatedOn));
            return ServiceResult<int>.Create(await _patientsRepository.Update(patient));
        }

        public async Task<ServiceResult<int>> UpdateRange(T[] patientsDto)
        {
            IEnumerable<Patient> existingPatients = _patientsRepository.GetRange(patientsDto.Select(patient => patient.Name.Id).ToArray());
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
            return ServiceResult<int>.Create(await _patientsRepository.UpdateRange(patients));
        }

        public ServiceResult<IEnumerable<T>> GetByDate(string dateDto)
        {
            FhirDate fhirDate = new FhirDate(dateDto);
            ValidationResult validationInfo = ValidationHelper.ValidateFhirDate(fhirDate);
            if (!validationInfo.IsValid)
            {
                return ServiceResult<IEnumerable<T>>.Create(validationInfo);
            }
            FhirDateTimeFilter fhirDateTimeFilter = new FhirDateTimeFilter(fhirDate);
            return ServiceResult<IEnumerable<T>>.Create(_patientsRepository.GetByDate(fhirDateTimeFilter.Get()).Select(patient => _mapper.Map<T>(patient)));
        }
    }
}
