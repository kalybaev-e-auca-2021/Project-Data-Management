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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
