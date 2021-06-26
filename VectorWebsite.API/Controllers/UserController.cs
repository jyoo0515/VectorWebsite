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

namespace VectorWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
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
