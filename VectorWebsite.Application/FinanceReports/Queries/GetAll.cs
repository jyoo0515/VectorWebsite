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

namespace VectorWebsite.Application.FinanceReports.Queries
{
    public class GetAll
    {
        public class Query : IRequest<List<FinanceReportDTO>>
        {
        }

        public class Handler : IRequestHandler<Query, List<FinanceReportDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<FinanceReportDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var financeReports = await _context.FinanceReports.Include(n => n.Creator).AsNoTracking().ToListAsync();

                var financeReportDTOs = _mapper.Map<List<FinanceReport>, List<FinanceReportDTO>>(financeReports);

                return financeReportDTOs;
            }
        }
    }
}
