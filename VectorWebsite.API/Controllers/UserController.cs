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
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    [Route("api/user")]
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
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterAsync(Register.Command command)
        {
            PortalLogin.Query query = new PortalLogin.Query
            {
                UserId = command.StudentId,
                Password = command.PortalPassword
            };

            bool confirm = await _mediator.Send(query);
            command.ConfirmedStudent = confirm;
            command.PortalPassword = null;

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
