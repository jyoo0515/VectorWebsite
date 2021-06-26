using Microsoft.AspNetCore.Identity;

namespace VectorWebsite.Infrastructure.Utils
{
    public interface IJwtGenerator
    {
        string CreateToken(IdentityUser user);
    }
}
