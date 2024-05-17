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
        public Guid Id {  get; set; }
        public Guid TeamLead {  get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public string ClientCompanyName {  get; set; }
        public string PerformerCompanyName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TeamLead, opt =>
                    opt.MapFrom(src => src.TeamLead))
                .ForMember(dest => dest.Priority, opt =>
                    opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.ClientCompanyName, opt =>
                    opt.MapFrom(src => src.ClientCompanyName))
                .ForMember(dest => dest.PerformerCompanyName, opt =>
                    opt.MapFrom(src => src.PerformerCompanyName))
                .ForMember(dest => dest.Id, opt => 
                    opt.MapFrom(src => src.Id));
        }
    }
}
