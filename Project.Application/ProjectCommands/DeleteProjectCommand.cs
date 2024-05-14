using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectCommands
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public Guid ProjectId { get; set; }
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public DeleteProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var changeProject = await _context.Projects
                .FindAsync(new object[] { command.ProjectId }, cancellationToken);
            if (changeProject == null)
            {
                throw new NotFoundException(nameof(changeProject), command.ProjectId);
            }
            _context.Projects.Remove(changeProject);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
