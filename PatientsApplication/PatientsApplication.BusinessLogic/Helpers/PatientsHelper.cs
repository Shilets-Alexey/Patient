using AutoMapper;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsApplication.BusinessLogic.Helpers
{
    public class PatientsHelper
    {
        public static T GetLookupValueByName<T>(IEnumerable<T> lookup, string name) where T : BaseLookup
        {
            return lookup.FirstOrDefault(lookupValue => lookupValue.Name.ToUpper() == name.ToUpper());
        }

        public static void AfterMap(IMappingOperationOptions<PatientDto, Patient> opt, IEnumerable<Gender> genders, IEnumerable<Active> actives, DateTime createdOn = default(DateTime))
        {
            opt.AfterMap((src, dest) => {
                Gender gender = GetLookupValueByName(genders, src.Gender);
                Active active = GetLookupValueByName(actives, src.Active.ToString());
                DateTime utcNow = DateTime.UtcNow;
                dest.GenderId = gender == default(Gender) ? null : gender.Id;
                dest.ActiveId = active == default(Active) ? null : active.Id;
                dest.ModifiedOn = utcNow;
                dest.CreatedOn = createdOn == default(DateTime) ? utcNow : createdOn;
            });
        }
    }
}
