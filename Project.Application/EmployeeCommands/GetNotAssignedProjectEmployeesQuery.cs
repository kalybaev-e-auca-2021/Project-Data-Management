using Application.Interfaces;
using Application.ProjectExtensions;
using AutoMapper;
using MediatR;
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
    public class GetNotAssignedProjectEmployeesQuery : IRequest<EmployeeListDto>
    {
        public Guid Id { get; set; }
    }

    public class GetNotAssignedProjectEmployeesQueryHandler
        : IRequestHandler<GetNotAssignedProjectEmployeesQuery, EmployeeListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetNotAssignedProjectEmployeesQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<EmployeeListDto> Handle(GetNotAssignedProjectEmployeesQuery request, CancellationToken cancellationToken)
        {
            // Retrieve project IDs associated with the employee
            var employeeIds = await _context.EmployeeProjects
                .Where(ep => ep.ProjectId == request.Id)
                .Select(ep => ep.EmployeeId)
                .ToListAsync(cancellationToken);

            // Retrieve projects based on the project IDs
            var employees = await _context.Employees
                .Where(p => !employeeIds.Contains(p.Id))
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // Create and populate the ProjectListDto
            return new EmployeeListDto { Employees =  employees};
        }
    }
}