using VectorWebsite.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace VectorWebsite.Persistance
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<PetitionComment> PetitionComments { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Proceeding> Proceedings { get; set; }
        public DbSet<FinanceReport> FinanceReports { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PetitionComment>()
                .HasOne(p => p.Petition)
                .WithMany(p => p.Comments);

            builder.Entity<Petition>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Petition)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
