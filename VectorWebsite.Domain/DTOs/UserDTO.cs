using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public Department Department { get; set; }
        public bool ConfirmedStudent { get; set; }
        public string StudentId { get; set; }
        public string Token { get; set; }
    }
}
