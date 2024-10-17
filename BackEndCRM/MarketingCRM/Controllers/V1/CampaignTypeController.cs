using Application.DTOs.Response;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingCRM.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CampaignTypeController : ControllerBase
    {
        private readonly IServiceCampaignTypes _service;

        public CampaignTypeController(IServiceCampaignTypes service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all campaign types.
        /// </summary>
        /// <response code="200"> Success </response>

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(GenericResponse))]
        public async Task<IActionResult> GetAllCampaignTypes()
        {
            var result = await _service.GetAllCampaignTypes();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}



