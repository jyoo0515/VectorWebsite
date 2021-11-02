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

namespace VectorWebsite.Application.FinanceReports.Commands
{
    public class Edit
    {
        public class Command : IRequest<FinanceReportDTO>
        {
            public FinanceReportDTO UpdatedFinanceReport { get; set; }
        }
        public class Handler : IRequestHandler<Command, FinanceReportDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<FinanceReportDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var financeReport = await _context.FinanceReports.FirstOrDefaultAsync(n => n.Id == request.UpdatedFinanceReport.Id);

                if (financeReport == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, "No such finance report exists");
                }

                _mapper.Map(request.UpdatedFinanceReport, financeReport);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                return request.UpdatedFinanceReport;
            }
        }
    }
}
