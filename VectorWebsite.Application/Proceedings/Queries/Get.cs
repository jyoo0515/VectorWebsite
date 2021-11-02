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

namespace VectorWebsite.Application.Proceedings.Queries
{
    public class Get
    {
        public class Query : IRequest<ProceedingDTO>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, ProceedingDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProceedingDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var proceeding = await _context.Proceedings
                    .Include(n => n.Creator)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(n => n.Id == request.Id);

                if (proceeding == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"{request.Id} is not found");
                }

                var proceedingDTO = _mapper.Map<Proceeding, ProceedingDTO>(proceeding);

                if (proceedingDTO == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError,
                        "Couldn't map proceeding to proceeding DTO");
                }

                return proceedingDTO;
            }
        }
    }
}
