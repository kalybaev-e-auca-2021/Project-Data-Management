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
    public class DeleteEmlpoyeeCommand : IRequest<Unit>
    {
        public Guid ProjectId { get; set; }
    }

    public class DeleteEmlpoyeeCommandHandler : IRequestHandler<DeleteEmlpoyeeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public DeleteEmlpoyeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteEmlpoyeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FindAsync(new object[] { command.ProjectId }, cancellationToken);
            if (employee == null)
            {
                throw new NotFoundException(nameof(employee), command.ProjectId);
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
