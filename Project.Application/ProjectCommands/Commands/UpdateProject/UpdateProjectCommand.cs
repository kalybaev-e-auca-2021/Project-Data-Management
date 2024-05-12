using Application.Interfaces;
using Application.ProjectCommands.Commands.CreateProject;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectCommands.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public int Priority { get; set; }
        public DateTime StartProjectDate { get; set; }
        public DateTime FinishProjectDate { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var changeProject = await _context.Projects
                .FirstOrDefaultAsync(cancellationToken);

            ArgumentNullException.ThrowIfNull(changeProject);

            if (changeProject.Name != command.Name)
            {
                changeProject.Name = command.Name;
            }
            if (changeProject.ClientCompanyName!= command.ClientCompanyName)
            {
                changeProject.ClientCompanyName = command.ClientCompanyName;
            }
            if (changeProject.PerformerCompanyName != command.PerformerCompanyName)
            {
                changeProject.PerformerCompanyName = command.PerformerCompanyName;
            }
            if (changeProject.Priority != command.Priority)
            {
                changeProject.Priority = (int) command.Priority;
            }
            if (changeProject.StartProjectDate != command.StartProjectDate)
            {
                changeProject.StartProjectDate = (DateTime)command.StartProjectDate;
            }
            if (changeProject.FinishProjectDate != command.FinishProjectDate)
            {
                changeProject.FinishProjectDate = (DateTime) command.FinishProjectDate;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return changeProject.Id;
        }
    }
}
