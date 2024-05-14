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
    public class EmployeeDto : IMapWith<Employee>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FirstName, opt =>
                    opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                    opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id));
        }
    }
}
