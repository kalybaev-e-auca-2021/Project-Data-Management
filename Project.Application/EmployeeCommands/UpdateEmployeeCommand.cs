using Application.Common;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.ProjectCommands
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SurName { get; set; }
        public string? Email { get; set; }
    }
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public UpdateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(r => r.Id == command.Id, cancellationToken);
            if (employee == null)
            {
                throw new NotFoundException(nameof(employee), command.Id);
            }
            if (command.FirstName != employee.FirstName)
            {
                employee.FirstName = command.FirstName;
            }

            if (command.LastName != employee.LastName)
            {
                employee.LastName = command.LastName;
            }

            if (command.SurName != employee.SurName)
            {
                employee.SurName = command.SurName;
            }

            if (command.Email != employee.SurName)
            {
                employee.Email = command.Email;
            }


            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(r => r.LastName)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(r => r.Email)
                .EmailAddress()
                .MaximumLength(250);
            RuleFor(r => r.SurName)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
