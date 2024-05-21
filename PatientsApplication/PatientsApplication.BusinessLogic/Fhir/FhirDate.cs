using System.Text.RegularExpressions;

namespace PatientsApplication.BusinessLogic.Fhir
{
    public class FhirDate
    {
        private Regex _dateRegexp = new Regex("([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)(-(0[1-9]|1[0-2])(-(0[1-9]|[1-2][0-9]|3[0-1]))?)?");

        private Regex _yearRegexp = new Regex("([0-9]([0-9]([0-9][1-9]|[1-9]0)|[1-9]00)|[1-9]000)");

        private Regex _monthRegexp = new Regex("-(0[1-9]|1[0-2])-");

        private Regex _dayRegexp = new Regex("((-(0[1-9]|[1-2][0-9]|3[0-1]))?)?$");

        private Regex _hourRegExp = new Regex("T([01][0-9]|2[0-3])");

        private Regex _minuteRegExp = new Regex(":[0-5][0-9]:|:[0-5][0-9]$");

        private Regex _secondRegExp = new Regex(":([0-5][0-9]|60)$|:([0-5][0-9]|60)\\.");

        private Regex _millisecondRegExp = new Regex("(\\.[0-9]{1,9})");

        private Regex _timezoneRegExp = new Regex("Z|(\\+|-)(0[0-9]|1[0-3]):[0-5][0-9]|14:00");

        private Regex _prefixRegexp = new Regex("^[a-z]{2}");

        public string Date { get; private set; }

        public string Prefix { get; private set; }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Day { get; private set; }

        public int? Hour { get; private set; }

        public int? Minute { get; private set; }

        public int? Second { get; private set; }

        public int? Millisecond { get; private set; }

        public string Timezone { get; private set; }

        public string ErrorMessage { get; private set; }

        public bool Success { get; private set; }

        public FhirDate(string date)
        {
            Create(date);
        }

        private void Create(string dateDtoWhithPrefix)
        {
            if (string.IsNullOrEmpty(dateDtoWhithPrefix))
            {
                Error(new ArgumentNullException(nameof(dateDtoWhithPrefix)).Message);
                return;
            }
            SetPrefix(dateDtoWhithPrefix);
            Date = string.IsNullOrEmpty(Prefix) ? dateDtoWhithPrefix : dateDtoWhithPrefix[2..dateDtoWhithPrefix.Length];
            string dateFormat = _dateRegexp.Match(Date).Value;
            if (string.IsNullOrEmpty(dateFormat))
            {
                Error("Invalid date");
                return;
            }
            SetYear(dateFormat);
            SetMonth(dateFormat);
            SetDay(dateFormat);
            SetHour(Date);
            SetMinute(Date);
            SetSecond(Date);
            SetMillisecond(Date);
            SetTimezone(Date);
            Success = true;
        }

        private void SetPrefix(string dateDto)
        {
            Prefix = _prefixRegexp.Match(dateDto).Value;
        }

        private void SetYear(string date)
        {
            string yearhStr = _yearRegexp.Match(date).Value;
            Year = string.IsNullOrEmpty(yearhStr) ? 0 : int.Parse(yearhStr);
        }

        private void SetMonth(string date)
        {
            string monthStr = _monthRegexp.Match(date).Value;
            monthStr = string.IsNullOrEmpty(monthStr) ? _dayRegexp.Match(date)?.Value : monthStr;
            Month = string.IsNullOrEmpty(monthStr) ? 0 : int.Parse(monthStr[1..3]);
        }

        private void SetDay(string date)
        {
            if (Month != 0 && !string.IsNullOrEmpty(_monthRegexp.Match(date).Value))
            {
                string dayStr = _dayRegexp.Match(date)?.Value;
                Day = string.IsNullOrEmpty(dayStr) ? 0 : int.Parse(dayStr[1..3]);
            }
        }

        private void SetHour(string dateDto)
        {
            if (Day != 0)
            {
                string hourStr = _hourRegExp.Match(dateDto)?.Value;
                Hour = string.IsNullOrEmpty(hourStr) ? null : int.Parse(hourStr[1..3]);
            }
        }

        private void SetMinute(string dateDto)
        {
            if (Hour != null)
            {
                string minuteStr = _minuteRegExp.Match(dateDto)?.Value;
                Minute = string.IsNullOrEmpty(minuteStr) ? null : int.Parse(minuteStr[1..3]);
            }
        }

        private void SetSecond(string dateDto)
        {
            if (Minute != null)
            {
                string secondStr = _secondRegExp.Match(dateDto)?.Value;
                Second = string.IsNullOrEmpty(secondStr) ? 0 : int.Parse(secondStr[1..3]);
            }
        }


        private void SetMillisecond(string dateDto)
        {
            if (Second != null)
            {
                string millisecondStr = _millisecondRegExp.Match(dateDto)?.Value;
                Millisecond = string.IsNullOrEmpty(millisecondStr) ? 0 : int.Parse(millisecondStr[1..millisecondStr.Length]);
            }
        }

        private void SetTimezone(string dateDto)
        {
            if (Millisecond != null)
            {
                Timezone = _timezoneRegExp.Match(dateDto)?.Value;
            }
        }

        private void Error(string message)
        {
            Success = false;
            ErrorMessage = message;
        }
    }
}
