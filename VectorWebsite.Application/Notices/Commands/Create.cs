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

namespace VectorWebsite.Application.Notices.Commands
{
    public class Create
    {
        public class Command : IRequest<NoticeDTO>
        {
            public string UserId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Category { get; set; }
            public IFormFile Attachment { get; set; }
        }
        public class Handler : IRequestHandler<Command, NoticeDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<NoticeDTO> Handle(Command request, CancellationToken cancellationToken)
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

                var notice = new Notice()
                {
                    Creator = user,
                    Title = request.Title,
                    Content = request.Content,
                    Category = request.Category,
                    FileName = filename
                };

                _context.Notices.Add(notice);

                bool success = await _context.SaveChangesAsync() > 0;

                if (!success)
                {
                    throw new Exception("Problem saving changes");
                }

                return _mapper.Map<Notice, NoticeDTO>(notice);
            }
        }
    }
}
