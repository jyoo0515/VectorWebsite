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
        public string Title { get; set; }
        public string Content { get; set; }
        //public Byte Attachment { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ClickNum { get; set; } = 0;
    }
}
