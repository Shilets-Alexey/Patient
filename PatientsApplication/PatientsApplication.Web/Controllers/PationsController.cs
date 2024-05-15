using Microsoft.AspNetCore.Mvc;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.BusinessLogic.Services;

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
        public ServiceResult<IEnumerable<PatientDto>> GetUsers()
        {
            return _patientsService.GetPatients();
        }

        [HttpPost]
        public async Task<ServiceResult<Guid>> CreatePatient(PatientDto patient)
        {
            return await _patientsService.CreatePatient(patient);
        }
    }
}
