using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Application.Proceedings.Queries;
using VectorWebsite.Application.Proceedings.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    // 회의록
    [Route("api/proceedings")]
    [ApiController]
    public class ProceedingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProceedingsController> _logger;

        public ProceedingsController(IMediator mediator, ILogger<ProceedingsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProceedingDTO>>> GetAllAsync()
        {
            var proceedings = await _mediator.Send(new GetAll.Query());

            return Ok(proceedings);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProceedingDTO>> GetAsync(Guid id)
        {
            var proceeding = await _mediator.Send(new Get.Query
            {
                Id = id
            });

            return Ok(proceeding);
        }

        [HttpPost]
        public async Task<ActionResult<ProceedingDTO>> CreateAsync([FromForm] Create.Command command)
        {
            var proceeding = await _mediator.Send(command);

            if (command.Attachment != null)
            {
                var result = await new FilesController().UploadFile(proceeding.Id, command.Attachment);
            }

            return Ok(proceeding);
        }

        [HttpPut]
        public async Task<ActionResult<NoticeDTO>> EditAsync([FromBody] ProceedingDTO updatedProceeding)
        {
            var command = new Edit.Command
            {
                UpdatedProceeding = updatedProceeding
            };

            var proceeding = await _mediator.Send(command);

            return Ok(proceeding);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id, string userId)
        {
            await _mediator.Send(new Delete.Command
            {
                Id = id,
                UserId = userId
            });

            return Ok();
        }
    }
}
