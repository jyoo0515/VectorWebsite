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
    public class Edit
    {
        public class Command : IRequest<InquiryDTO>
        {
            public InquiryDTO UpdatedInquiry { get; set; }
        }
        public class Handler : IRequestHandler<Command, InquiryDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<InquiryDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var inquiry = await _context.Inquiries.FirstOrDefaultAsync(i => i.Id == request.UpdatedInquiry.Id);

                if (inquiry == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, "No such inquiry exists");
                }

                if (inquiry.Status != InquiryStatus.등록)
                {
                    throw new Exception("Cannot edit inquiry at this status");
                }

                _mapper.Map(request.UpdatedInquiry, inquiry);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                return request.UpdatedInquiry;
            }
        }
    }
}
