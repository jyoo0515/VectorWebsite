using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VectorWebsite.Domain;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Infrastructure.Utils;

namespace VectorWebsite.Application.Users.Queries
{
    public class GetCurrentUser
    {
        public class Query : IRequest<UserDTO>
        {
        }

        public class Handler : IRequestHandler<Query, UserDTO>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserAccessor _userAccessor;
            public Handler(UserManager<ApplicationUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._jwtGenerator = jwtGenerator;
                this._userManager = userManager;
            }

            public async Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                //어차피 /api/user는 authorize 되어 있어서, 애초에 적합한 정보를 가진 유저만이 이 Handle메소드까지 도달할 수 있다.
                //따라서 user가 null일 걱정은 안해도 된다.
                var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    StudentId = user.StudentId,
                    Token = _jwtGenerator.CreateToken(user),
                    ConfirmedStudent = user.ConfirmedStudent,
                    IsAdmin = user.IsAdmin,
                };

                throw new Exception("Problem Getting user data");
            }
        }
    }
}
