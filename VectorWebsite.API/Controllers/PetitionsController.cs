﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VectorWebsite.Domain;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Application.Petitions.Queries;
using VectorWebsite.Application.Petitions.Commands;
using VectorWebsite.Application.Petitions.Comments;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    [Route("api/petitions")]
    [ApiController]
    public class PetitionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PetitionsController> _logger;

        public PetitionsController(IMediator mediator, ILogger<PetitionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<PetitionDTO>>> GetAllAsync()
        {
            var petitions = await _mediator.Send(new GetAll.Query());

            return Ok(petitions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PetitionDTO>> GetAsync(Guid id)
        {
            var petition = await _mediator.Send(new Get.Query
            {
                Id = id
            });

            return Ok(petition);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] PetitionDTO petition)
        {
            var command = new Create.Command
            {
                Petition = petition
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

    [Route("api/petitions/comments")]
    [ApiController]
    public class PetitionCommentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PetitionCommentsController> _logger;

        public PetitionCommentsController(IMediator mediator, ILogger<PetitionCommentsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCommentAsync(string userId, string content, Guid petitionId)
        {
            var command = new CreateComment.Command
            {
                UserId = userId,
                Content = content,
                PetitionId = petitionId
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommentAsync(Guid id)
        {
            await _mediator.Send(new DeleteComment.Command
            {
                Id = id
            });

            return Ok();
        }
    }
}
