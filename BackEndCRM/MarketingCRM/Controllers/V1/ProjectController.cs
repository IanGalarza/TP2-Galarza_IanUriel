using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Exceptions;
using Application.Interfaces.Service;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingCRM.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IServiceProjects _service;

        public ProjectController(IServiceProjects service)
        {
            _service = service;
        }
        /// <summary>
        /// Retrieves a list of projects based on the provided filters such as project name, campaign type, client, with optional pagination parameters.
        /// </summary>
        /// <response code="200"> Success </response>
        /// <response code="400"> Bad Request </response>
      
        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(ProjectResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(ApiError))]

        public async Task<IActionResult> GetAllProjects(string? name, int? campaign, int? client, int? offset, int? size)
        {
            try
            {
                var result = await _service.GetAllProjects(name, campaign, client, offset, size);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (InvalidArgumentsException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        /// <summary>
        /// Creates a new project with the specified details.
        /// </summary>
        /// <response code="201"> Success </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="409"> Conflict </response>

        [HttpPost]
        [ProducesResponseType(statusCode: 201, type: typeof(ProjectDetails))]
        [ProducesResponseType(statusCode: 400, type: typeof(ApiError))]
        [ProducesResponseType(statusCode: 409, type: typeof(ApiError))]
        public async Task<IActionResult> CreateProject(ProjectRequest request)
        {
            try
            {
                var result = await _service.CreateProject(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (InvalidArgumentsException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (Conflict ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 409 };
            }
        }

        /// <summary>
        /// Retrieves detailed information about a specific project by its ID.
        /// </summary>
        /// <response code="200"> Success </response>
        /// <response code="404"> Not Found </response>

        [HttpGet("/api/v1/Project/{id}")]
        [ProducesResponseType(statusCode: 200, type: typeof(ProjectDetails))]
        [ProducesResponseType(statusCode: 404, type: typeof(ApiError))]
        public async Task<IActionResult> GetByIdProject(Guid id)
        {
            try
            {
                var result = await _service.GetByIdProject(id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (InvalidValueException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
            }
        }

        /// <summary>
        /// Adds a new interaction to an existing project.
        /// </summary>
        /// <response code="201"> Success </response>
        /// <response code="400"> Bad Request </response>

        [HttpPatch("/api/v1/Project/{id}/interactions")]
        [ProducesResponseType(statusCode: 201, type: typeof(InteractionsResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(ApiError))]

        public async Task<IActionResult> AddNewInteraction(Guid id, InteractionsRequest request)
        {
            try
            {
                var result = await _service.AddNewInteraction(id, request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (InvalidValueException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (InvalidArgumentsException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        /// <summary>
        /// Adds a new task to an existing project.
        /// </summary>
        /// <response code="201"> Success </response>
        /// <response code="400"> Bad Request </response>

        [HttpPatch("/api/v1/Project/{id}/tasks")]
        [ProducesResponseType(statusCode: 201, type: typeof(TasksResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(ApiError))]

        public async Task<IActionResult> AddNewTask(Guid id, TasksRequest request)
        {
            try
            {
                var result = await _service.AddNewTask(id, request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (InvalidValueException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (InvalidArgumentsException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        /// <summary>
        /// Updates an existing task with the specified details.
        /// </summary>
        /// <response code="200"> Success </response>
        /// <response code="400"> Bad Request </response>

        [HttpPut("/api/v1/Tasks/{id}")]
        [ProducesResponseType(statusCode: 200, type: typeof(TasksResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(ApiError))]

        public async Task<IActionResult> UpdateTask(Guid id, TasksRequest request)
        {
            try
            {
                var result = await _service.UpdateTasks(id, request);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (InvalidValueException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
            catch (InvalidArgumentsException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }
    }
}
