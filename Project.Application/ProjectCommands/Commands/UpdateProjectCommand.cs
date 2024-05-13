﻿using Application.Common;
using Application.Interfaces;
using Application.ProjectCommands.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectCommands.Commands
{
    public class UpdateProjectCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public int Priority { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var changeProject = await _context.Projects
                .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);
            if (changeProject == null)
            {
                throw new NotFoundException(nameof(changeProject), command.Id);
            }
            changeProject.Name = command.Name;
            changeProject.ClientCompanyName = command.ClientCompanyName;
            changeProject.PerformerCompanyName = command.PerformerCompanyName;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}