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

namespace VectorWebsite.Application.Petitions.Commands
{
    public class Delete
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
                var petition = await _context.Petitions.FirstOrDefaultAsync(p => p.Id == request.Id);

                if (petition == null)
                {
                    throw new Exception($"Petition {request.Id} is not found");
                }

                _context.Petitions.Remove(petition);
                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Error deleting petition");
                }

                return Unit.Value;
            }
        }
    }
}
