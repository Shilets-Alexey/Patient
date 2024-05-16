using Microsoft.AspNetCore.Mvc;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatientsApplication.Web.Controllers
{
    [Route("patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientsService _patientsService;

        public PatientsController(PatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet]
        public ServiceResult<IEnumerable<PatientDto>> GetPatients()
        {
            return _patientsService.GetPatients();
        }

        [HttpGet, Route("date")]
        public ServiceResult<IEnumerable<PatientDto>> GetPatientsByDate(string birthday)
        {
            return _patientsService.GetPatients();
        }

        [HttpGet, Route("range")]
        public ServiceResult<IEnumerable<PatientDto>> GetPatients(Guid[] patientsId)
        {
            return _patientsService.GetPatients(patientsId);
        }

        [HttpGet, Route("{patientId}")]
        public ServiceResult<PatientDto> GetPatient(Guid patientId)
        {
            return _patientsService.GetPatient(patientId);
        }

        [HttpPost]
        public async Task<ServiceResult<Guid>> CreatePatient(PatientDto patient)
        {
            return await _patientsService.CreatePatient(patient);
        }

        [HttpPost, Route("range")]
        public async Task<ServiceResult<IEnumerable<Guid>>> CreatePatients(PatientDto [] patients)
        {
            return await _patientsService.CreatePatients(patients);
        }

        [HttpDelete, Route("{patientId}")]
        public async Task<ServiceResult<int>> DeletePatient(Guid patientId)
        {
            return await _patientsService.DeletePatient(patientId);
        }

        [HttpDelete, Route("range")]
        public async Task<ServiceResult<int>> DeletePatients(Guid[] patientsId)
        {
            return await _patientsService.DeletePatients(patientsId);
        }

        [HttpPut]
        public async Task<ServiceResult<int>> UpdatePatient(PatientDto patient)
        {
            return await _patientsService.UpdatePatient(patient);
        }

        [HttpPut, Route("range")]
        public async Task<ServiceResult<int>> UpdatePatient(PatientDto[] patients)
        {
            return await _patientsService.UpdatePatients(patients);
        }
    }
}
