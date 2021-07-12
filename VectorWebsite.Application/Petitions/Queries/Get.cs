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

namespace VectorWebsite.Application.Petitions.Queries
{
    public class Get
    {
        public class Query : IRequest<PetitionDTO>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, PetitionDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PetitionDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var petition = await _context.Petitions
                    .Include(p => p.Comments)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (petition == null)
                {
                    throw new Exception($"{request.Id} is not found");
                }
                petition.Comments.OrderBy(c => c.CreatedDate);

                var petitionDTO = _mapper.Map<Petition, PetitionDTO>(petition);

                if (petitionDTO == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError,
                        "Couldn't map notice to petition DTO");
                }

                return petitionDTO;
            }
        }
    }
}
