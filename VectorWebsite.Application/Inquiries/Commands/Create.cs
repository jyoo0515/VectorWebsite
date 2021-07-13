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

namespace VectorWebsite.Application.Inquiries.Commands
{
    public class Create
    {
        public class Command : IRequest
        {
            public InquiryDTO Inquiry { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var inquiry = _mapper.Map<Inquiry>(request.Inquiry);

                if (inquiry == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError, "Failed to map inquiry");
                }

                _context.Inquiries.Add(inquiry);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                return Unit.Value;
            }
        }
    }
}
