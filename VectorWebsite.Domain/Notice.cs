using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain
{
    public class Notice
    {
        public Guid Id { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; init; } = DateTime.Now;
        public int ClickNum { get; set; } = 0;
    }
}
