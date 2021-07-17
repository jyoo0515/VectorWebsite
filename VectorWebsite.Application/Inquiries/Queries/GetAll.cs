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

namespace VectorWebsite.Application.Inquiries.Queries
{
    public class GetAll
    {
        public class Query : IRequest<List<InquiryDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<InquiryDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<InquiryDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var inquiries = await _context.Inquiries
                    .Include(i => i.Creator)
                    .AsNoTracking()
                    .OrderBy(i => i.CreatedDate)
                    .ToListAsync();

                var inquiryDTOs = _mapper.Map<List<Inquiry>, List<InquiryDTO>>(inquiries);

                return inquiryDTOs;
            }
        }
    }
}
