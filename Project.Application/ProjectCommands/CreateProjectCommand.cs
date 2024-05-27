using Application.Interfaces;
using Application.ProjectCommands;
using Domain.Entities;
using FluentValidation;
using MediatR;


namespace Application.ProjectCommands
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string ClientCompanyName { get; set; }
        public string PerformerCompanyName { get; set; }
        public string Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
                StartProjectDate = command.StartDate,
                FinishProjectDate = command.EndDate,
            };

            await _context.Projects.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
    }
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty();
            RuleFor(r => r.ClientCompanyName)
                .NotEmpty();
            RuleFor(r => r.PerformerCompanyName)
                .NotEmpty();
            RuleFor(r => r.Priority)
                .NotEmpty();
        }
    }
}
