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

namespace VectorWebsite.Application.Proceedings.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
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
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.UserId);
                if (!user.IsAdmin)
                {
                    throw new Exception("User is not an admin");
                }

                var proceeding = await _context.Proceedings.FirstOrDefaultAsync(n => n.Id == request.Id);

                if (proceeding == null)
                {
                    throw new Exception($"Proceeding {request.Id} is not found");
                }

                _context.Proceedings.Remove(proceeding);
                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Error deleting a proceeding");
                }

                return Unit.Value;
            }
        }
    }
}
