using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Application.ProjectExtensions;
using Domain.Entities;
namespace Application.ProjectCommands.Commands
{
    public class GetAllProjectQuery : IRequest<ProjectLookUpDto>
    {
        [JsonIgnore] public ProjectLookUpDto projects { get; set; } = new();
        public Guid Id {  get; set; }
    }

    public class GetAllProjectQueryHandler
        : IRequestHandler<GetAllProjectQuery, ProjectLookUpDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllProjectQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProjectLookUpDto> Handle(
            GetAllProjectQuery request,
            CancellationToken cancellationToken)
        {
            var projects = GetProjects();
            //request.projects.Projects = await projects
            //    .ProjectTo<ProjectLookUpDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync(cancellationToken);
            return request.projects;

        }
        private IQueryable<Project> GetProjects()
        {
            return _context.Projects
                .AsNoTracking();
        }
    }
}