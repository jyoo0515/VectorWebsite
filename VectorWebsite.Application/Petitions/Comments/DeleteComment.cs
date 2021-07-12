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

namespace VectorWebsite.Application.Petitions.Comments
{
    public class DeleteComment
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var comment = await _context.PetitionComments
                    .Include(p => p.Petition)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (comment == null)
                {
                    throw new Exception($"Petition comment {request.Id} is not found");
                }

                if (comment.Petition.AgreeNum > 0)
                {
                    comment.Petition.AgreeNum -= 1;
                }
                _context.PetitionComments.Remove(comment);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Error deleting comment");
                }

                return Unit.Value;
            }
        }
    }
}
