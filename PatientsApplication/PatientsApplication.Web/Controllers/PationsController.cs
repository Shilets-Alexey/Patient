using Microsoft.AspNetCore.Mvc;
using PatientsApplication.BusinessLogic.Interfaces;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.BusinessLogic.Services;
using PatientsApplication.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace PatientsApplication.Web.Controllers
{

    /// <summary>
    /// Patient controller
    /// </summary>
    [Route("patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IEntityService<PatientDto> _patientsService;

        /// <summary>
        /// Patient constructor
        /// </summary>
        public PatientsController(IEntityService<PatientDto> patientsService)
        {
            _patientsService = patientsService;
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResult<IEnumerable<PatientDto>> Get()
        {
            return _patientsService.Get();
        }

        /// <summary>
        /// Get patients range
        /// </summary>
        /// <param name="patientsId">patient ids</param>
        /// <returns></returns>
        [HttpGet, Route("range")]
        public ServiceResult<IEnumerable<PatientDto>> GetRange(Guid[] patientsId)
        {
            return _patientsService.GetRange(patientsId);
        }

        /// <summary>
        /// Get patient by id
        /// </summary>
        /// <param name="patientId">patient id</param>
        /// <returns></returns>
        [HttpGet, Route("{patientId}")]
        public ServiceResult<PatientDto> GetById(Guid patientId)
        {
            return _patientsService.GetById(patientId);
        }

        /// <summary>
        /// Create patient
        /// </summary>
        /// <param name="patient">patient info</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServiceResult<Guid>> Create(PatientDto patient)
        {
            return await _patientsService.Create(patient);
        }

        /// <summary>
        /// Create patients range
        /// </summary>
        /// <param name="patients">patients</param>
        /// <returns></returns>
        [HttpPost, Route("range")]
        public async Task<ServiceResult<IEnumerable<Guid>>> CreateRange(PatientDto [] patients)
        {
            return await _patientsService.CreateRange(patients);
        }

        /// <summary>
        /// Dalete patient
        /// </summary>
        /// <param name="patientId">patient id</param>
        /// <returns></returns>
        [HttpDelete, Route("{patientId}")]
        public async Task<ServiceResult<int>> Delete(Guid patientId)
        {
            return await _patientsService.Delete(patientId);
        }

        /// <summary>
        /// Dalete patients range
        /// </summary>
        /// <param name="patientsId">patient ids</param>
        /// <returns></returns>
        [HttpDelete, Route("range")]
        public async Task<ServiceResult<int>> DeleteRange(Guid[] patientsId)
        {
            return await _patientsService.DeleteRange(patientsId);
        }

        /// <summary>
        /// Update patient info
        /// </summary>
        /// <param name="patient">patient info</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ServiceResult<int>> Update(PatientDto patient)
        {
            return await _patientsService.Update(patient);
        }

        /// <summary>
        /// Update patiens
        /// </summary>
        /// <param name="patients">patients range</param>
        /// <returns></returns>
        [HttpPut, Route("range")]
        public async Task<ServiceResult<int>> UpdateRange(PatientDto[] patients)
        {
            return await _patientsService.UpdateRange(patients);
        }

        /// <summary>
        /// Get Patients by birthDay
        /// </summary>
        /// <param name="dateDto">date in string with prefix</param>
        /// <returns></returns>
        [HttpGet, Route("date")]
        public ServiceResult<IEnumerable<PatientDto>> GetByDate(string dateDto)
        {
            if (string.IsNullOrEmpty(dateDto))
            {
                throw new ArgumentNullException("dateDto");
            }
            return _patientsService.GetByDate(dateDto);
        }
    }
}



