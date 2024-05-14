using Application.ProjectCommands.Commands;
using Application.ProjectExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers
{
    [Route("api/")]
    public class ProjectController : BaseController
    {
        /// <summary>
        /// Контроллер чтобы получить список всех проектов
        /// </summary>
        [HttpGet("api/getProjects")]
        [ProducesResponseType(typeof(ProjectListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = new GetProjectListQuery();
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("api/createProject")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProject()
        {
            var command = new CreateProjectCommand();
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("api/deleteProject")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject([FromQuery]Guid Id)
        {
            var command = new DeleteProjectCommand { ProjectId = Id };
            return Ok(await Mediator.Send(command));
        }

    }
}