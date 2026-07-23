using Microsoft.EntityFrameworkCore;

namespace DrAshrafMellouli.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed some initial data or configure any specific relations if needed
        }
    }
}
