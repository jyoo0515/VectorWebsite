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
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace VectorWebsite.Application.FinanceReports.Commands
{
    public class Create
    {
        public class Command : IRequest<FinanceReportDTO>
        {
            public string UserId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Category { get; set; }
            public IFormFile Attachment { get; set; }
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
                var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == request.UserId);
                string filename = null;

                if (!user.IsAdmin)
                {
                    throw new Exception("User is not an admin");
                }

                if (request.Attachment != null)
                {
                    filename = request.Attachment.FileName;
                }

                var financeReport = new FinanceReport()
                {
                    Creator = user,
                    Title = request.Title,
                    Content = request.Content,
                    Category = request.Category,
                    FileName = filename
                };

                _context.FinanceReports.Add(financeReport);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                return _mapper.Map<FinanceReport, FinanceReportDTO>(financeReport);
            }
        }
    }
}
