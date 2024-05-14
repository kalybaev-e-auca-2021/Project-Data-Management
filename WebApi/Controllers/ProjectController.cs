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
        [HttpGet("api/getProjectList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProjects()
        {
            var query = new GetProjectListQuery();
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Gets the list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/getEmployeeList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetEmployeeListQuery();
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Creates a Project
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/createProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProject([FromQuery] CreateProjectCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Creates a Project
        /// </summary>
        /// <returns></returns>
        [HttpPost("api/createEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateEmployee([FromQuery] CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a project by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("api/deleteProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject([FromQuery] Guid Id)
        {
            var command = new DeleteProjectCommand { ProjectId = Id };
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes an Employee by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("api/deleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid Id)
        {
            var command = new DeleteProjectCommand { ProjectId = Id };
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Gets the details of the project by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/getProjectDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectDetails([FromQuery] Guid Id)
        {
            var query = new GetProjectDetailsQuery { Id = Id };
            return Ok(await Mediator.Send(query));

        }

        /// <summary>
        /// Gets the details of the employee by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/getEmployeeDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeDetails([FromQuery] Guid Id)
        {
            var query = new GetEmployeeDetailsQuery { Id = Id };
            return Ok(await Mediator.Send(query));

        }
    }
}