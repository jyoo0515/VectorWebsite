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

namespace VectorWebsite.Application.Notices.Queries
{
    public class Get
    {
        public class Query : IRequest<NoticeDTO>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, NoticeDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<NoticeDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var notice = await _context.Notices.FirstOrDefaultAsync(n => n.Id == request.Id);

                if (notice == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"{request.Id} is not found");
                }

                var noticeDTO = _mapper.Map<Notice, NoticeDTO>(notice);

                if (noticeDTO == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError,
                        "Couldn't map notice to notice DTO");
                }

                return noticeDTO;
            }
        }
    }
}
