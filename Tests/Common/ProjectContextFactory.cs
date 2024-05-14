using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Tests.Common
{
    public class ProjectContextFactory
    {
        public static Guid ProjectIdForDelete = Guid.NewGuid();
        public static Guid ProjectIdForUpdate = Guid.NewGuid();

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Projects.AddRange(new Project 
            {
                Name = "TestCompany",
                ClientCompanyName = "OpenAI",
                PerformerCompanyName = "FaceBook",
                Priority = "High",
                Id = Guid.Parse("6A11B992-A97B-41A4-970E-692F5B0D23E8"),
                StartProjectDate = DateTime.Today,
                FinishProjectDate = DateTime.Today,
            },
            new Project
            {
                Name = "TestCompany2",
                ClientCompanyName = "OpenAI2",
                PerformerCompanyName = "FaceBook2",
                Priority = "High",
                Id = Guid.Parse("B08B786E-B8DC-4052-9777-F1CD1D462955"),
                StartProjectDate = DateTime.Today,
                FinishProjectDate = DateTime.Today,
            },
            new Project
            {
                Name = "TestCompany3",
                ClientCompanyName = "OpenAI3",
                PerformerCompanyName = "FaceBook3",
                Priority = "High",
                Id = ProjectIdForDelete,
                StartProjectDate = DateTime.Today,
                FinishProjectDate = DateTime.Today,
            },
            new Project
            {
                Name = "TestCompany4",
                ClientCompanyName = "OpenAI4",
                PerformerCompanyName = "FaceBook4",
                Priority = "High",
                Id = ProjectIdForUpdate,
                StartProjectDate = DateTime.Today,
                FinishProjectDate = DateTime.Today,
            }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();  
        }
    }
}
