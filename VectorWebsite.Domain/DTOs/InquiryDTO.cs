using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain.DTOs
{
    public class InquiryDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; init; }
        public bool IsPrivate { get; set; }
        public string FileName { get; set; }
        public InquiryStatus Status { get; set; }
    }
}