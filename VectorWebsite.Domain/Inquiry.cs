using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain
{
    public class Inquiry
    {
        public Guid Id { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; init; } = DateTime.Now;
        public bool IsPrivate { get; set; }
        public InquiryStatus Status { get; set; } = InquiryStatus.등록;
    }

    public enum InquiryStatus
    {
        등록,
        접수,
        답변완료
    }
}