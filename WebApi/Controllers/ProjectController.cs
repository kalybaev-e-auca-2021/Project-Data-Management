using Application.ProjectCommands.Commands;
using Application.ProjectExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers
{
    [Route("api/")]
    public class ProjectController : BaseController
    {
        [HttpGet("api/getProjects")]
        [ProducesResponseType(typeof(ProjectLookUpDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = new GetAllProjectQuery();
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("api/createProject")]
        [ProducesResponseType(typeof(ProjectLookUpDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProject()
        {
            var command = new CreateProjectCommand();
            return Ok(await Mediator.Send(command));
        }
    }
}