using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCDT52CW2Models;

namespace SCDT52CW2Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Assets> Assets { get; set; }
        public DbSet<GeneralIssue> GeneralIssues { get; set; }
        public DbSet<TechnicalIssue> TechnicalIssues { get; set; }
        public DbSet<Update> Updates { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
