using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.ProjectCommands
{
    public class AssignEmployeeToProjectCommand : IRequest<Unit>
    {
        public Guid EmployeeId{ get; set; }
        public Guid ProjectId {  get; set; }
    }
    public class AssignEmployeeToProjectCommandHandler : IRequestHandler<AssignEmployeeToProjectCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public AssignEmployeeToProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(AssignEmployeeToProjectCommand command, CancellationToken cancellationToken)
        {
            var entity = new EmployeeProject
            {
                EmployeeId = command.EmployeeId,
                ProjectId = command.ProjectId
            };
            _context.EmployeeProjects.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
