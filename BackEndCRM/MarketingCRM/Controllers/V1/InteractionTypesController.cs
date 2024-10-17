using Application.DTOs.Response;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingCRM.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InteractionTypesController : ControllerBase
    {

        private readonly IServiceInteractionTypes _service;

        public InteractionTypesController(IServiceInteractionTypes service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all interaction types.
        /// </summary>
        /// <response code="200"> Success </response>


        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(GenericResponse))]
        public async Task<IActionResult> GetAllInteractionTypes()
        {
            var result = await _service.GetAllInteractionTypes();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
