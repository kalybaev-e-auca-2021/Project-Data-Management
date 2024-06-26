﻿using Application.Common;
using Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProjectCommands
{
    public class UpdateProjectCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ClientCompanyName { get; set; }
        public string? PerformerCompanyName { get; set; }
        public string? Priority { get; set; }
        public DateTime? StartProjectDate { get; set; }
        public DateTime? FinishProjectDate { get; set; }
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
            if(command.Name != changeProject.Name)
            {
                changeProject.Name = command.Name;
            }
            if (command.ClientCompanyName != changeProject.ClientCompanyName)
            {
                changeProject.ClientCompanyName = command.ClientCompanyName;
            }

            if (command.PerformerCompanyName != changeProject.PerformerCompanyName)
            {
                changeProject.PerformerCompanyName = command.PerformerCompanyName;
            }

            if (command.Priority != changeProject.Priority)
            {
                changeProject.Priority = command.Priority;
            }

            if (command.StartProjectDate != changeProject.StartProjectDate)
            {
                changeProject.StartProjectDate = (DateTime)command.StartProjectDate;
            }

            if (command.FinishProjectDate != changeProject.FinishProjectDate)
            {
                changeProject.FinishProjectDate = (DateTime)command.FinishProjectDate;
            }


            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
            RuleFor(r => r.Name)
                .NotEmpty();
            RuleFor(r => r.FinishProjectDate)
                .NotEmpty();
            RuleFor(r => r.StartProjectDate)
                .NotEmpty();
            RuleFor(r => r.PerformerCompanyName)
                .NotEmpty();
            RuleFor(r => r.ClientCompanyName)
                .NotEmpty();
            RuleFor(r => r.Priority)
                .NotEmpty();
        }
    }
}
