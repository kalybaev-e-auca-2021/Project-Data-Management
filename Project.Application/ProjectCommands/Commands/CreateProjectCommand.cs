using Application.Interfaces;
using Domain.Entities;
using MediatR;


namespace Application.ProjectCommands.Commands
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public int Priority { get; set; }
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
                Name = "command.Name",
                ClientCompanyName = "command.ClientCompanyName",
                PerformerCompanyName = "command.PerformerCompanyName",
                Priority = 1,
                StartProjectDate = DateTime.UtcNow,
                FinishProjectDate = DateTime.UtcNow,
            };

            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
    }
}
