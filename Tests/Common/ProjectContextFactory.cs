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
                Priority = 1,
                Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                StartProjectDate = DateTime.Today,
                FinishProjectDate = DateTime.Today,
            },
            new Project
            {
                Name = "TestCompany2",
                ClientCompanyName = "OpenAI2",
                PerformerCompanyName = "FaceBook2",
                Priority = 1,
                Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B826"),
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
