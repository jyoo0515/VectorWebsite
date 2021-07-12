using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain
{
    public class Petition
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public int AgreeNum { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
        public List<PetitionComment> Comments { get; set; } = new List<PetitionComment>();
    }

    public class PetitionComment
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual Petition Petition { get; set; }
    }
}
