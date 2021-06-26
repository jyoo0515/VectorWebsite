using MediatR;
using VectorWebsite.Domain;
using VectorWebsite.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace VectorWebsite.Application.Users.Queries
{
    public class GetAll
    {
        public class Query : IRequest<List<ApplicationUser>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ApplicationUser>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<ApplicationUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.ApplicationUsers.ToListAsync();
            }
        }
    }
}
