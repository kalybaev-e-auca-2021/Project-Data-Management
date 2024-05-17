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
    public class GetProjectEmployeesQuery : IRequest<EmployeeListDto>
    {
        public Guid ProjectId { get; set; }
    }

    public class GetProjectEmployeesQueryHandler
        : IRequestHandler<GetProjectEmployeesQuery, EmployeeListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProjectEmployeesQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<EmployeeListDto> Handle(GetProjectEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeIds = await _context.EmployeeProjects
                .Where(ep => ep.ProjectId == request.ProjectId)
                .Select(ep => ep.ProjectId)
                .ToListAsync(cancellationToken);

            var employees = await _context.Employees
                .Where(p => employeeIds.Contains(p.Id))
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var dto = new EmployeeListDto
            {
                Employees = employees
            };

            return dto;
        }
    }
}