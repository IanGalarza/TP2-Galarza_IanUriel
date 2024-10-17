﻿using Application.DTOs.Response;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketingCRM.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceUsers _service;

        public UserController(IServiceUsers service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <response code="200"> Success </response>

        [HttpGet]
        [ProducesResponseType(statusCode: 200, type: typeof(UsersResponse))]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _service.GetAllUsers();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
