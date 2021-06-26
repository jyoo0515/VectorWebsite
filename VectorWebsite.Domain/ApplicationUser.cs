using System;
using Microsoft.AspNetCore.Identity;

namespace VectorWebsite.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool ConfirmedStudent { get; set; }
        public string Token { get; set; }
    }
}
