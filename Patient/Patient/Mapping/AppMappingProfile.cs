using AutoMapper;
using PatientApplication.Models;
using PatientsApplication.DataAccess.Entities;
using System;

namespace AuthorizationApp.BusinessLogic.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {

            CreateMap<Patient, PatientDto>()
                .ForPath(dest => dest.Name.Id, opt => opt.MapFrom(patien => patien.Id))
                .ForPath(dest => dest.Name.Use, opt => opt.MapFrom(patien => patien.Use))
                .ForPath(dest => dest.Name.Family, opt => opt.MapFrom(patien => patien.Family))
                .ForPath(dest => dest.Name.Given, opt => opt.MapFrom(patien => string.IsNullOrWhiteSpace(patien.Given) ? new string[0] : patien.Given.Split(',', StringSplitOptions.None)))
                .ForMember("BirthDate", opt => opt.MapFrom(patien => patien.BirthDate));
            CreateMap<PatientDto, Patient>()
                .ForMember("Id", opt => opt.MapFrom(patien => patien.Name.Id))
                .ForMember("Family", opt => opt.MapFrom(patien => patien.Name.Family))
                .ForMember("Use", opt => opt.MapFrom(patien => patien.Name.Use))
                .ForMember("Given", opt => opt.MapFrom(patien => string.Join(',', patien.Name.Given)))
                .ForMember("BirthDate", opt => opt.MapFrom(patien => patien.BirthDate));
        }
    }
}