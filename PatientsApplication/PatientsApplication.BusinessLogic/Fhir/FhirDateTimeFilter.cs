using PatientsApplication.DataAccess.Entities;
using System.Globalization;

namespace PatientsApplication.BusinessLogic.Fhir
{
    public class FhirDateTimeFilter
    {
        private FhirDate _fhirDate;

        private DateTime _startDate;

        private DateTime _endDate;

        public FhirDateTimeFilter(FhirDate fhirDate)
        {
            _fhirDate = fhirDate;
        }


        public Func<Patient, bool> Get()
        {
            if (string.IsNullOrEmpty(_fhirDate.Prefix))
            {
                return Equal;
            }
            return GetFilterByPrefix();
        }
        private bool Equal(Patient patient)
        {
            return patient.BirthDate == DateTime.Parse(_fhirDate.Date).ToUniversalTime();
        }

        private Func<Patient, bool> GetFilterByPrefix()
        {
            SetStartAndEndDate();
            Func<Patient, bool> filterNethod;
            switch (_fhirDate.Prefix)
            {
                case "eq":
                    filterNethod = GetEqFilterMethod;
                    break;
                case "ne":
                    filterNethod = GetNeFilterMethod;
                    break;
                case "gt":
                    filterNethod = GetGtFilterMethod;
                    break;
                case "lt":
                    filterNethod = GetLtFilterMethod;
                    break;
                case "ge":
                    filterNethod = GetGeFilterMethod;
                    break;
                case "le":
                    filterNethod = GetLeFilterMethod;
                    break;
                default:
                    filterNethod = Equal;
                    break;
            }
            return filterNethod;
        }
        private void SetStartAndEndDate()
        {
            (DateTime startDate, DateTime endDate) = GetStartAndEndDate();
            _startDate = startDate;
            _endDate = endDate;
        }
        private bool GetEqFilterMethod(Patient patient)
        {
            
            return patient.BirthDate >= _startDate && patient.BirthDate <_endDate;
        }

        private bool GetNeFilterMethod(Patient patient)
        {
            return patient.BirthDate < _startDate || patient.BirthDate >= _endDate;
        }

        private bool GetGtFilterMethod(Patient patient)
        {
            return patient.BirthDate >= _endDate;
        }

        private bool GetLtFilterMethod(Patient patient)
        {
            return patient.BirthDate < _startDate;
        }

        private bool GetGeFilterMethod(Patient patient)
        {
            return patient.BirthDate >= _startDate;
        }

        private bool GetLeFilterMethod(Patient patient)
        {
            return patient.BirthDate < _endDate;
        }

        private (DateTime, DateTime) GetStartAndEndDate()
        {
            string methodeName = GetMethodName();
            DateTime startDate = GetStartDate();
            DateTime endDate = GetEndDate(startDate, methodeName);
            return (startDate, endDate);

        }

        private DateTime GetStartDate()
        {
            if(_fhirDate.Month == 0)
            {
                return new DateTime(_fhirDate.Year, 1, 1).ToUniversalTime();
            }
            if (_fhirDate.Minute == null && _fhirDate.Hour != null)
            {
                return new DateTime(_fhirDate.Year, _fhirDate.Month, _fhirDate.Day, (int)_fhirDate.Hour, 0, 0).ToUniversalTime();
            }
            return DateTime.Parse(_fhirDate.Date).ToUniversalTime();
        }

        private string GetMethodName()
        {
            if (_fhirDate.Millisecond != null)
            {
                return "millisecond";
            }
            if (_fhirDate.Second != null)
            {
                return "second";
            }
            if (_fhirDate.Minute != null)
            {
                return "minute";
            }
            if (_fhirDate.Hour != null)
            {
                return "hour";
            }
            if (_fhirDate.Day != 0)
            {
                return "day";
            }
            if (_fhirDate.Month != 0)
            {
                return "month";
            }

            return "year";
        }
        private DateTime GetEndDate(DateTime startDate, string methodeName)
        {
            DateTime endDate = startDate;
            switch (methodeName)
            {
                case "year":
                    endDate = endDate.AddYears(1);
                    break;
                case "month":
                    endDate = endDate.AddMonths(1);
                    break;
                case "day":
                    endDate = endDate.AddDays(1);
                    break;
                case "hour":
                    endDate = endDate.AddHours(1);
                    break;
                case "minute":
                    endDate = endDate.AddMinutes(1);
                    break;
                case "second":
                    endDate = endDate.AddSeconds(1);
                    break;
                case "millisecond":
                    endDate = endDate.AddMilliseconds(1);
                    break;
                default:
                    return endDate;
            }
            return endDate;
        }
    }
}
