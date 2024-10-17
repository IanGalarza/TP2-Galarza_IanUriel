using Application.DTOs.Response;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MarketingCRM.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly IServiceTaskStatus _service;

        public TaskStatusController(IServiceTaskStatus service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all task statuses.
        /// </summary>
        /// <response code="200"> Success </response>

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(GenericResponse))]

        public async Task<IActionResult> GetAllTaskStatus()
        {
            var result = await _service.GetAllTaskStatus();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}

