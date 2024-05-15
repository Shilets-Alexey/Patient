using PatientApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsApplication.Exceptions
{
    [Serializable]
    public class PatientsException<T> : Exception
    {
        public ServiceResult<T> serviceResult {get; set;}

        public PatientsException(ServiceResult<T> serviceResult)
        {
            this.serviceResult = serviceResult;
        }
    }
}
