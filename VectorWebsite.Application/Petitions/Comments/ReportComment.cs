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


namespace VectorWebsite.Application.Petitions.Comments
{
    public class ReportComment
    {
        public class Command : IRequest
        {
            public Guid CommentId { get; set; }
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
                var comment = await _context.PetitionComments.Include(p => p.Petition).FirstOrDefaultAsync(p => p.Id == request.CommentId);

                if (comment == null)
                {
                    throw new Exception($"Petition comment {request.CommentId} is not found");
                }

                comment.Report += 1;
                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Error saving changes");
                }

                return Unit.Value;
            }
        }
    }
}
