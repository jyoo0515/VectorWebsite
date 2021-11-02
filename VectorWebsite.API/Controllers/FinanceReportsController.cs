using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Application.FinanceReports.Queries;
using VectorWebsite.Application.FinanceReports.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    // 결산안
    [Route("api/financereports")]
    [ApiController]
    public class FinanceReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FinanceReportsController> _logger;

        public FinanceReportsController(IMediator mediator, ILogger<FinanceReportsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FinanceReportDTO>>> GetAllAsync()
        {
            var financeReports = await _mediator.Send(new GetAll.Query());

            return Ok(financeReports);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<FinanceReportDTO>> GetAsync(Guid id)
        {
            var financeReport = await _mediator.Send(new Get.Query
            {
                Id = id
            });

            return Ok(financeReport);
        }

        [HttpPost]
        public async Task<ActionResult<FinanceReportDTO>> CreateAsync([FromForm] Create.Command command)
        {
            var financeReport = await _mediator.Send(command);

            if (command.Attachment != null)
            {
                var result = await new FilesController().UploadFile(financeReport.Id, command.Attachment);
            }

            return Ok(financeReport);
        }

        [HttpPut]
        public async Task<ActionResult<FinanceReportDTO>> EditAsync([FromBody] FinanceReportDTO updatedFinanceReport)
        {
            var command = new Edit.Command
            {
                UpdatedFinanceReport = updatedFinanceReport
            };

            var financeReport = await _mediator.Send(command);

            return Ok(financeReport);
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
