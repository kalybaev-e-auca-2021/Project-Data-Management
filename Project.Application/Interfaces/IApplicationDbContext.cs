using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Project> Projects { get; set; }

        DbSet<Employee> Employees { get; set; }

        DbSet<EmployeeProject> EmployeeProjects { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
