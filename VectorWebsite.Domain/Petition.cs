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
        public ApplicationUser Creator { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AgreeNum { get; set; } = 0;
        public DateTime CreatedDate { get; init; } = DateTime.Now;
        public DateTime ExpiryDate { get; init; } = DateTime.Now.AddMonths(2);
        public List<PetitionComment> Comments { get; set; } = new List<PetitionComment>();
    }

    public class PetitionComment
    {
        public Guid Id { get; set; }
        public ApplicationUser Creator { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; init; } = DateTime.Now;
        public Petition Petition { get; set; }
    }
}
