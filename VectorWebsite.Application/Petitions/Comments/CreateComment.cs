using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using VectorWebsite.Domain;
using VectorWebsite.Persistance;
using Microsoft.EntityFrameworkCore;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Infrastructure.Exceptions;
using System.Diagnostics;

namespace VectorWebsite.Application.Petitions.Comments
{
    public class CreateComment
    {
        public class Command : IRequest<PetitionCommentDTO>
        {
            public string UserId { get; set; }
            public string Content { get; set; }
            public Guid PetitionId { get; set; }
        }
        public class Handler : IRequestHandler<Command, PetitionCommentDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PetitionCommentDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.UserId);
                var petition = await _context.Petitions
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.Id == request.PetitionId);

                var comment = new PetitionComment
                {
                    Creator = user,
                    Content = request.Content,
                    Petition = petition
                };

                if (comment == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError, "Failed to map petition comment");
                }
                petition.Comments.Add(comment);
                petition.AgreeNum += 1;

                _context.PetitionComments.Add(comment);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                var commentDTO = _mapper.Map<PetitionComment, PetitionCommentDTO>(comment);

                return commentDTO;
            }
        }
    }
}
