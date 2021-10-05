using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorWebsite.Infrastructure.Exceptions;
using VectorWebsite.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using VectorWebsite.Domain;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using System.Net;
using FluentValidation;
using VectorWebsite.Application.Validators;

namespace VectorWebsite.Application.Users.Commands
{
    public class Register
    {
        public class Command : IRequest<UserDTO>
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string StudentId { get; set; }
            public int Department { get; set; }
            public string PhoneNumber { get; set; }
            public string PortalPassword { get; set; }
            public bool ConfirmedStudent { get; set; }
            //이것은 사용하지 않음. Admin은 직접 설정한다.
            public bool IsAdmin { get; set; } = false;
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, UserDTO>
        {
            private readonly DataContext _context;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(DataContext context, UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator)
            {
                this._userManager = userManager;
                this._jwtGenerator = jwtGenerator;
                this._context = context;
            }

            public async Task<UserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(u => u.Email == request.Email).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
                }

                if (await _context.Users.Where(u => u.UserName == request.Username).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { UserName = "UserName already exists" });
                }

                var user = new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    StudentId = request.StudentId,
                    Department = (Department)request.Department,
                    PhoneNumber = request.PhoneNumber,
                    IsAdmin = false,
                    ConfirmedStudent = request.ConfirmedStudent,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new UserDTO
                    {
                        Id = user.Id,
                        IsAdmin = user.IsAdmin,
                        UserName = user.UserName,
                        ConfirmedStudent = user.ConfirmedStudent,
                        Token = _jwtGenerator.CreateToken(user),
                    };
                }

                throw new Exception("Problem Creating User");
            }
        }
    }
}
