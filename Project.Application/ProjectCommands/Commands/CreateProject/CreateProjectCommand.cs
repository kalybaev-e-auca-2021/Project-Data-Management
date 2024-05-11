using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;


namespace Application.ProjectCommands.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public int Priority { get; set; }
        public DateTime StartProjectDate { get; set; }
        public DateTime FinishProjectDate { get; set; }
    }
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                ClientCompanyName = command.ClientCompanyName,
                PerformerCompanyName = command.PerformerCompanyName,
                Priority = command.Priority,
                StartProjectDate = command.StartProjectDate,
                FinishProjectDate = command.FinishProjectDate,
            };

            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
    }
}
