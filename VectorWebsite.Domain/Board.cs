using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain
{
    class Board
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ApplicationUser Creator { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
