using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Application.Notices.Queries;
using VectorWebsite.Application.Notices.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NoticesController> _logger;

        public NoticesController(IMediator mediator, ILogger<NoticesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoticeDTO>>> GetAllAsync()
        {
            var notices = await _mediator.Send(new GetAll.Query());

            return Ok(notices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoticeDTO>> GetAsync(Guid id)
        {
            var notice = await _mediator.Send(new Get.Query
            {
                Id = id
            });

            return Ok(notice);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] NoticeDTO notice)
        {
            var command = new Create.Command
            {
                Notice = notice
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync([FromBody] NoticeDTO updatedNotice)
        {
            var command = new Edit.Command
            {
                UpdatedNotice = updatedNotice
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new Delete.Command
            {
                Id = id
            });

            return Ok();
        }
    }
}
