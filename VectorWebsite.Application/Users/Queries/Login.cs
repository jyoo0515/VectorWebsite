using MediatR;
using VectorWebsite.Domain;
using VectorWebsite.Infrastructure.Exceptions;
using VectorWebsite.Infrastructure.Utils;
using VectorWebsite.Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace VectorWebsite.Application.Users.Queries
{
    public class Login
    {
        public class Query : IRequest<UserDTO>
        {
            public string Id { get; set; }
            public string Password { get; set; }
        }
        //public class Handler : IRequestHandler<Query, UserDTO>
        //{
        //    private readonly SignInManager<IdentityUser> _signInManager;
        //    private readonly UserManager<ApplicationUser> _userManager;
        //    private readonly IJwtGenerator _jwtGenerator;

        //    public Handler(SignInManager<IdentityUser> signInManager, UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator)
        //    {
        //        _signInManager = signInManager;
        //        _userManager = userManager;
        //        _jwtGenerator = jwtGenerator;
        //    }

        //    public async Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
        //    {
        //        if (string.IsNullOrEmpty(request.Id) || string.IsNullOrEmpty(request.Password))
        //        {
        //            throw new RestException(HttpStatusCode.BadRequest, "Id and Password cannot be empty");
        //        }

        //        var user = await _userManager.FindByIdAsync(request.Id);

        //        if (user == null)
        //        {
        //            throw new RestException(HttpStatusCode.Unauthorized);
        //        }

        //        var result = await _signInManager
        //            .CheckPasswordSignInAsync(user, request.Password, false);

        //        if (result.Succeeded)
        //        {
        //            return new UserDTO
        //            {
        //                Name = user.UserName,
        //                IsAdmin = user.IsAdmin,
        //                ConfirmedStudent = user.ConfirmedStudent,
        //                Token = _jwtGenerator.CreateToken(user),
        //            };
        //        }

        //        throw new RestException(HttpStatusCode.Unauthorized, "Id or Password is incorrect.");
        //    }
        //}
    }
}
