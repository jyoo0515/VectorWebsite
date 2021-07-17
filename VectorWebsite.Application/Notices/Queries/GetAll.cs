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

namespace VectorWebsite.Application.Notices.Queries
{
    public class GetAll
    {
        public class Query : IRequest<List<NoticeDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<NoticeDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<NoticeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var notices = await _context.Notices.Include(n => n.Creator).AsNoTracking().ToListAsync();

                var noticeDTOs = _mapper.Map<List<Notice>, List<NoticeDTO>>(notices);

                return noticeDTOs;
            }
        }
    }
}
