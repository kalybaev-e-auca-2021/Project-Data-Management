using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Application.ProjectExtensions;
using Domain.Entities;
namespace Application.ProjectCommands
{
    public class GetEmployeeProjetcsQuery : IRequest<ProjectListDto>
    {
        public Guid EmployeeId { get; set; }
    }

    public class GetEmployeeProjetcsQueryHandler
        : IRequestHandler<GetEmployeeProjetcsQuery, ProjectListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeProjetcsQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProjectListDto> Handle(GetEmployeeProjetcsQuery request, CancellationToken cancellationToken)
        {
            // Retrieve project IDs associated with the employee
            var projectIds = await _context.EmployeeProjects
                .Where(ep => ep.EmployeeId == request.EmployeeId)
                .Select(ep => ep.ProjectId)
                .ToListAsync(cancellationToken);

            // Retrieve projects based on the project IDs
            var projects = await _context.Projects
                .Where(p => projectIds.Contains(p.Id))
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // Create and populate the ProjectListDto
            var dto = new ProjectListDto
            {
                Projects = projects
            };

            return dto;
        }
    }
}