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

namespace VectorWebsite.Application.Inquiries.Commands
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
                var inquiry = await _context.Inquiries.FirstOrDefaultAsync(i => i.Id == request.Id);

                if (inquiry == null)
                {
                    throw new Exception($"Inquiry {request.Id} is not found");
                }

                _context.Inquiries.Remove(inquiry);
                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Error deleting notice");
                }

                return Unit.Value;
            }
        }
    }
}
