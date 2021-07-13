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
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool ConfirmedStudent { get; set; }
        public string Token { get; set; }
    }
}
