using AutoMapper;
using PatientsApplication.BusinessLogic.Models;
using PatientsApplication.DataAccess.Entities;
using System;

namespace PatientsApplication.BusinessLogic.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {

            CreateMap<Patient, PatientDto>()
                .ForPath(dest => dest.Name.Id, opt => opt.MapFrom(patient => patient.Id))
                .ForPath(dest => dest.Name.Use, opt => opt.MapFrom(patient => patient.Use))
                .ForPath(dest => dest.Name.Family, opt => opt.MapFrom(patient => patient.Family))
                .ForPath(dest => dest.Name.Given, opt => opt.MapFrom(patient => string.IsNullOrWhiteSpace(patient.Given) ? new string[0] : patient.Given.Split(',', StringSplitOptions.None)))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(patient => patient.Active.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(patient => patient.Gender.Name));
            CreateMap<PatientDto, Patient>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(patient => patient.Name.Id))
                .ForMember(dest => dest.Family, opt => opt.MapFrom(patient => patient.Name.Family))
                .ForMember(dest => dest.Use, opt => opt.MapFrom(patient => patient.Name.Use))
                .ForMember(dest => dest.Given, opt => opt.MapFrom(patient => string.Join(',', patient.Name.Given)))
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Gender, opt => opt.Ignore());
        }
    }
}
