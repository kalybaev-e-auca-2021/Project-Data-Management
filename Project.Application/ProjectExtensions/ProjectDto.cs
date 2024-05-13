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
    public class ProjectDto : IMapWith<Project>
    {
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public int Priority { get; set; }
        public DateOnly StartProjectDate { get; set; }
        public DateOnly FinishProjectDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ClientCompanyName, opt =>
                    opt.MapFrom(src => src.ClientCompanyName))
                .ForMember(dest => dest.PerformerCompanyName, opt =>
                    opt.MapFrom(src => src.PerformerCompanyName))
                .ForMember(dest => dest.Priority, opt =>
                    opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.StartProjectDate, opt =>
                    opt.MapFrom(src => src.StartProjectDate))
                .ForMember(dest => dest.FinishProjectDate, opt =>
                    opt.MapFrom(src => src.FinishProjectDate));
        }
    }
}
