using GeekHunters.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekHunters.Persistence
{
    public class GeekHuntersDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        
        public GeekHuntersDbContext(DbContextOptions<GeekHuntersDbContext> options) : base(options)
        {
        }
        
        
        
    }
    
    public class GeekHuntersSqlLiteDbContext : DbContext
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        
        public GeekHuntersSqlLiteDbContext(DbContextOptions<GeekHuntersSqlLiteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<CandidateSkill>().HasAlternateKey(cs => new {cs.CandidateId, cs.SkillId});
            
        }
    }
}