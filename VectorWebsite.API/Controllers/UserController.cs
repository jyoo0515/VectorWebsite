using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Application.Users.Queries;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("login")]
        public async Task<ActionResult<UserDTO>> ActionResult([FromQuery] string id, [FromQuery] string password)
        {
            var user = await _mediator.Send(new Login.Query
            {
                Id = id,
                Password = password
            });

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
