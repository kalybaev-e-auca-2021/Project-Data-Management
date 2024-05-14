using Application.Common;
using Application.ProjectCommands.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace Tests.UnitTests
{
    public class UpdateProjectCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task UpdateProjectCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateProjectCommandHandler(Context);
            var updateDetails = "new Details";

            //Act
            await handler.Handle(new UpdateProjectCommand
            {
                Id = ProjectContextFactory.ProjectIdForUpdate,
                Name = updateDetails,
                ClientCompanyName = updateDetails,
                PerformerCompanyName = updateDetails,
                Priority = 2
            }, CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Projects.SingleOrDefaultAsync(r => r.Id == ProjectContextFactory.ProjectIdForUpdate
            && r.Name == updateDetails && r.ClientCompanyName == updateDetails && r.PerformerCompanyName == updateDetails && r.Priority == 2));
        }

        [Fact]
        public async Task UpdateProjectCommandHandler_FaileOnWrongId()
        {
            //Arrange
            var handler = new UpdateProjectCommandHandler(Context);

            //Act
            //Assert

            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
                new UpdateProjectCommand
                {
                    Id = Guid.NewGuid(),

                }, CancellationToken.None));
        }
    }
}
