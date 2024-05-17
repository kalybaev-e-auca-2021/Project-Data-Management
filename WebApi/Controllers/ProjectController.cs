using Application.ProjectCommands;
using Application.ProjectExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProjectController : BaseController
    {
        /// <summary>
        /// Gets the list of projects
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProjectList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectListDto>> GetAllProjects()
        {
            var query = new GetProjectListQuery();
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Gets the list of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEmployeeList")]
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
        [HttpPost("createProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> CreateProject([FromBody]CreateProjectCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Creates a Project
        /// </summary>
        /// <returns></returns>
        [HttpPost("createEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a project by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("deleteProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject(Guid Id)
        {
            var command = new DeleteProjectCommand { ProjectId = Id };
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Deletes an Employee by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("deleteEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmployee(Guid Id)
        {
            var command = new DeleteEmlpoyeeCommand { EmployeeId = Id };
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Updates a Project by Id
        /// </summary>
        /// <returns></returns>
        [HttpPost("updateProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates an Employee by Id
        /// </summary>
        /// <returns></returns>
        [HttpPost("updateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Gets the details of the project by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProjectDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectDetails([FromQuery]Guid Id)
        {
            var query = new GetProjectDetailsQuery { Id = Id };
            return Ok(await Mediator.Send(query));

        }

        /// <summary>
        /// Gets the details of the employee by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEmployeeDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeDetails([FromQuery]Guid Id)
        {
            var query = new GetEmployeeDetailsQuery { Id = Id };
            return Ok(await Mediator.Send(query));

        }

        /// <summary>
        /// Assigns EmployeeToProject
        /// </summary>
        /// <returns></returns>
        [HttpPost("assignEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AssignEmployee([FromQuery] Guid EmployeeId, Guid ProjectId)
        {
            var command = new AssignEmployeeToProjectCommand { EmployeeId = EmployeeId, ProjectId = ProjectId };
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets the projects of employee working on by EmployeeId
        /// </summary>
        /// <returns></returns>
        [HttpGet("getEmployeeProjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeProjects([FromQuery] Guid Id)
        {
            var query = new GetEmployeeProjetcsQuery { EmployeeId = Id };
            return Ok(await Mediator.Send(query));

        }

        [HttpGet("getNotAssignedProjectEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotAssignedEmployeeProjects([FromQuery] Guid Id)
        {
            var query = new GetNotAssignedProjectEmployeesQuery { Id = Id };
            return Ok(await Mediator.Send(query));

        }

        /// <summary>
        /// Gets the employees of a project by projectId
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProjectEmployees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectEmployees([FromQuery] Guid Id)
        {
            var query = new GetProjectEmployeesQuery { ProjectId = Id };
            return Ok(await Mediator.Send(query));

        }
    }
}