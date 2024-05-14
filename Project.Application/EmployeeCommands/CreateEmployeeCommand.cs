using Application.Interfaces;
using Application.ProjectCommands;
using Domain.Entities;
using FluentValidation;
using MediatR;


namespace Application.ProjectCommands
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
    }
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                SurName = command.SurName,
                Email = command.Email,
            };

            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return employee.Id;
        }
    }
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(r => r.LastName)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(r => r.SurName)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(r => r.Email)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
