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

namespace VectorWebsite.Application.Proceedings.Queries
{
    public class GetAll
    {
        public class Query : IRequest<List<ProceedingDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<ProceedingDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ProceedingDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var proceedings = await _context.Proceedings.Include(n => n.Creator).AsNoTracking().ToListAsync();

                var proceedingDTOs = _mapper.Map<List<Proceeding>, List<ProceedingDTO>>(proceedings);

                return proceedingDTOs;
            }
        }
    }
}
