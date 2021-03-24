using System;
using Microsoft.AspNetCore.Identity;

namespace VectorWebsite.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool IsStudent { get; set; }
    }
}
