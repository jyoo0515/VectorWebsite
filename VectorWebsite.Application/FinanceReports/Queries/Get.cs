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

namespace VectorWebsite.Application.FinanceReports.Queries
{
    public class Get
    {
        public class Query : IRequest<FinanceReportDTO>
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, FinanceReportDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<FinanceReportDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var financeReport = await _context.FinanceReports
                    .Include(n => n.Creator)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(n => n.Id == request.Id);

                if (financeReport == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, $"{request.Id} is not found");
                }

                var financeReportDTO = _mapper.Map<FinanceReport, FinanceReportDTO>(financeReport);

                if (financeReportDTO == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.InternalServerError,
                        "Couldn't map finance report to finance report DTO");
                }

                return financeReportDTO;
            }
        }
    }
}
