using Application.ProjectCommands;
using Application.ProjectExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/")]
    public class ProjectController : BaseController
    {
        /// <summary>
        /// Gets the list of projects
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/getProjects")]
        [ProducesResponseType(typeof(ProjectListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = new GetProjectListQuery();
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Creates a Project
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/createProject")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProject()
        {
            var command = new CreateProjectCommand();
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes a project by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("api/deleteProject")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject([FromQuery]Guid Id)
        {
            var command = new DeleteProjectCommand { ProjectId = Id };
            return Ok(await Mediator.Send(command));
        }

    }
}