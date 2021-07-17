using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VectorWebsite.Domain;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Application.Inquiries.Queries;
using VectorWebsite.Application.Inquiries.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    [Route("api/inquiries")]
    [ApiController]
    public class InquiriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InquiriesController> _logger;

        public InquiriesController(IMediator mediator, ILogger<InquiriesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<InquiryDTO>>> GetAllAsync()
        {
            var petitions = await _mediator.Send(new GetAll.Query());

            return Ok(petitions);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<InquiryDTO>> GetAsync(Guid id)
        {
            var petition = await _mediator.Send(new Get.Query
            {
                Id = id
            });

            return Ok(petition);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] Create.Command command)
        {
            if (command.Attachment != null)
            {
                var result = await new FilesController().UploadFile("Inquiry", command.Attachment);
            }

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> EditAsync([FromBody] InquiryDTO updatedInquiry)
        {
            var command = new Edit.Command
            {
                UpdatedInquiry = updatedInquiry
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
