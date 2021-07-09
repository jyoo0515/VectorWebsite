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

namespace VectorWebsite.Application.Notices.Commands
{
    public class Edit
    {
        public class Command : IRequest
        {
            public NoticeDTO UpdatedNotice { get; set; }
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
                var notice = await _context.Notices.FirstOrDefaultAsync(n => n.Id == request.UpdatedNotice.Id);

                if (notice == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, "No such notice exists");
                }

                _mapper.Map(request.UpdatedNotice, notice);

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
