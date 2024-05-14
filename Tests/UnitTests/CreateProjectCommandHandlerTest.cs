using Xunit;
using Application.ProjectCommands.Commands;
using Microsoft.EntityFrameworkCore;
using Tests.Common;

namespace Tests.UnitTests;

public class CreateProjectCommandHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CreateProjectCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateProjectCommandHandler(Context);

        //Act
        var projectName = "project name ";
        var projectDetails = "project details";
        var projectId = await handler.Handle(new CreateProjectCommand
        {
            Name = projectName,
            ClientCompanyName = projectDetails,
            PerformerCompanyName = projectDetails,
            Priority = 1,
            StartDate = DateTime.Today,
            EndDate = DateTime.Today
        }, CancellationToken.None);

        //Assert
        Assert.NotNull(await Context.Projects.SingleOrDefaultAsync(r => r.Id == projectId && r.ClientCompanyName == projectDetails
        && r.PerformerCompanyName == projectDetails
        && r.Priority == 1));
    }
}
