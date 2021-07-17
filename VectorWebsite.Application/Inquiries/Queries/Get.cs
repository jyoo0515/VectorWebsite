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

namespace VectorWebsite.Application.Inquiries.Queries
{
    public class Get
    {
        public class Query : IRequest<InquiryDTO>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, InquiryDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<InquiryDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var inquiry = await _context.Inquiries
                    .Include(i => i.Creator)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(i => i.Id == request.Id);

                if (inquiry == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"{request.Id} is not found");
                }

                var inquiryDTO = _mapper.Map<Inquiry, InquiryDTO>(inquiry);

                if (inquiryDTO == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError,
                        "Couldn't map inquiry to inquiry DTO");
                }

                return inquiryDTO;
            }
        }
    }
}
