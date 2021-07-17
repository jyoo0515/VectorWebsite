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

namespace VectorWebsite.Application.Petitions.Queries
{
    public class GetAll
    {
        public class Query : IRequest<List<PetitionDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<PetitionDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<PetitionDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var petitions = await _context.Petitions
                    .Include(p => p.Creator)
                    .Include(p => p.Comments)
                    .AsNoTracking()
                    .OrderBy(p => p.CreatedDate)
                    .ToListAsync();

                var petitionDTOs = _mapper.Map<List<Petition>, List<PetitionDTO>>(petitions);

                return petitionDTOs;
            }
        }
    }
}
