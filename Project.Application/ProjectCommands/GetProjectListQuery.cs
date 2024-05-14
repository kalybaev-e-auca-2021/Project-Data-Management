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
    public class GetProjectListQuery : IRequest<ProjectListDto>
    {
    }

    public class GetProjectListQueryHandler
        : IRequestHandler<GetProjectListQuery, ProjectListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProjectListQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ProjectListDto> Handle(
            GetProjectListQuery request,
            CancellationToken cancellationToken)
        {
            var projectQuery = await _context.Projects
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new ProjectListDto { Projects = projectQuery };

        }
    }
}