using Application.Common;
using Application.Interfaces;
using Application.ProjectExtensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectCommands
{
    public class GetEmployeeDetailsQuery : IRequest<EmployeeDetailsDto>
    {
        public Guid Id { get; set; }
    }

    public class GetEmployeeDetailsHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetEmployeeDetailsHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailsQuery command, CancellationToken cancellationToken)
        {
            var entity = await _context.Employees
                .Where(r => r.Id == command.Id)
                .ProjectTo<EmployeeDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(entity), command.Id);
            }
            return entity;
        }
    }
}
