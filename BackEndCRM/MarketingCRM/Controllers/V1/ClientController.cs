using Application.DTOs.Response;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Application.Exceptions;
using Application.DTOs.Request;
using Application.Interfaces.Service;

namespace MarketingCRM.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IServiceClients _service;

        public ClientController(IServiceClients service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <response code="200"> Success </response>

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(ClientsResponse))]
        public async Task<IActionResult> GetAllClients()
        {
            var result = await _service.GetAllClients();
            return new JsonResult(result) { StatusCode = 200 };
        }

        /// <summary>
        /// Creates a new client with the provided details.
        /// </summary>
        /// <response code="201"> Success </response>
        /// <response code="400"> Bad Request </response>

        [HttpPost]
        [ProducesResponseType(statusCode: 201, type: typeof(ClientsResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(ApiError))]
        public async Task<IActionResult> CreateClient(ClientsRequest request)
        {
            try
            {
                var result = await _service.CreateClient(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (InvalidArgumentsException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message}) { StatusCode = 400 };
            }
        }
    }
}

