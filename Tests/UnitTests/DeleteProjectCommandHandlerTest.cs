using Xunit;
using Application.ProjectCommands.Commands;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using Application.Common;

namespace Tests.UnitTests
{
    public class DeleteProjectCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteProjectCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteProjectCommandHandler(Context);
            
            //Act
            await handler.Handle(new DeleteProjectCommand
            {
                ProjectId = ProjectContextFactory.ProjectIdForDelete,
            }, CancellationToken.None);

            //Assert
            Assert.Null(Context.Projects.SingleOrDefault(r => r.Id == ProjectContextFactory.ProjectIdForDelete));
        }

        [Fact]
        public async Task DeleteProjectCommandHandler_FailOnWrongId()
        {
            var handler = new DeleteProjectCommandHandler(Context);
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(new DeleteProjectCommand{
                ProjectId = Guid.NewGuid()
            },CancellationToken.None));   
        }
    }
}
