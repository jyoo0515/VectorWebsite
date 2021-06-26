using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWebsite.Infrastructure.Exceptions;
using VectorWebsite.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace VectorWebsite.Application.Users.Commands
{
    public class Register
    {
        public class Command : IRequest
        {
            public string Name { get; set; }
        }

        //public class Handler : IRequestHandler<Command>
        //{
        //    private readonly DataContext _context;
        //    public Handler(DataContext context)
        //    {
        //        _context = context;
        //    }
        //}
    }
}
