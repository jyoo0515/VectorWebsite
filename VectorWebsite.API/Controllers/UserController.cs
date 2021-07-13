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
using VectorWebsite.Application.Users.Commands;

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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginAsync(Login.Query query)
        {
            var user = await _mediator.Send(query);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(Register.Command command)
        {
            return await _mediator.Send(command);
        }

        //현재 로그인한 유저 정보 반환
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            return await _mediator.Send(new GetCurrentUser.Query());
        }

    }
}
