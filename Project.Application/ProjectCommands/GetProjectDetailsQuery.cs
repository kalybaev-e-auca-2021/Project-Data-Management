using Application.Common;
using Application.Interfaces;
using Application.ProjectExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectCommands
{
    public class GetProjectDetailsQuery : IRequest<ProjectDto>
    {
        public Guid Id { get; set; }
    }

    public class GetProjectDetailsQueryHandler : IRequestHandler<GetProjectDetailsQuery, ProjectDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProjectDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProjectDto> Handle(GetProjectDetailsQuery command, CancellationToken cancellationToken)
        {
            var entity = _context.Projects
                .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(entity), command.Id);
            }
            return _mapper.Map<ProjectDto>(entity);
        }
    }
}
