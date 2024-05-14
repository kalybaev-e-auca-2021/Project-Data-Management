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
    public class GetEmployeeListQuery : IRequest<EmployeeListDto>
    {
    }

    public class GetEmployeeListQueryHandler
        : IRequestHandler<GetEmployeeListQuery, EmployeeListDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeListQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<EmployeeListDto> Handle(
            GetEmployeeListQuery request,
            CancellationToken cancellationToken)
        {
            var query = await _context.Employees
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new EmployeeListDto { Employees = query };

        }
    }
}