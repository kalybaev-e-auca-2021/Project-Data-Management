using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using AutoMapper;

namespace Application.ProjectExtensions
{
    public class EmployeeDetailsDto : IMapWith<Employee>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.FirstName, opt =>
                    opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                    opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.SurName, opt =>
                    opt.MapFrom(src => src.SurName))
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id));
        }
    }
}
