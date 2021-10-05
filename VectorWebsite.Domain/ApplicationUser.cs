using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VectorWebsite.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool ConfirmedStudent { get; set; }
        public string Token { get; set; }
        public string StudentId { get; set; }
        public Department Department { get; set; }
    }

    public enum Department
    {
        신소재공학과 = 1,
        화공생명공학과 = 2,
        기계4반 = 4,
        컴퓨터과학과 = 5,
        전기전자6반 = 6,
        기계7반 = 7,
        산업공학과 = 8,
        전기전자9반 = 9,
        전기전자10반 = 10,
        사회환경시스템공학과 = 11,
        건축공학과 = 12,
        도시공학과 = 13,
        글로벌융합공학부 = 14,
        시스템반도체공학과 = 15,
    }
}
