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
    public class Edit
    {
        public class Command : IRequest<ProceedingDTO>
        {
            public ProceedingDTO UpdatedProceeding { get; set; }
        }
        public class Handler : IRequestHandler<Command, ProceedingDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ProceedingDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var proceeding = await _context.Proceedings.FirstOrDefaultAsync(n => n.Id == request.UpdatedProceeding.Id);

                if (proceeding == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, "No such proceeding exists");
                }

                _mapper.Map(request.UpdatedProceeding, proceeding);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                return request.UpdatedProceeding;
            }
        }
    }
}
