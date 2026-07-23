using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DrAshrafMellouli.Models
{
    public enum Category
    {
        Body,
        Face,
        Hair,
        Laser
    }

    public class Treatment
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        public string? ImageUrl { get; set; }
        
        [Required]
        public Category Category { get; set; }
        
        public string? ProtocolDetails { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        
        [Required]
        public string CaseNumber { get; set; } = string.Empty; // e.g., #H-440
        
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public Category Category { get; set; }
        
        public string? BeforeImageUrl { get; set; }
        public string? AfterImageUrl { get; set; }
        
        public string? ProtocolName { get; set; }
        public string? Sessions { get; set; }
        public string? ResultType { get; set; } // e.g., Durable
    }

    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Procedure { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; } = 5;
        public string Initials => string.Join("", Name.Split(' ').Select(s => s[0])).ToUpper();
        public bool IsFeatured { get; set; }
    }

    public class Article
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public string? CoverImageUrl { get; set; }

        public string? CategoryName { get; set; }
        
        public DateTime DatePublished { get; set; } = DateTime.Now;
    }

    public class HomeViewModel
    {
        public List<Result> Results { get; set; } = new();
        public List<Article> Articles { get; set; } = new();
        public List<Testimonial> Testimonials { get; set; } = new();
    }

    public class Appointment
    {
        public int Id { get; set; }
        
        [Required]
        public string FullName { get; set; } = string.Empty;
        
        public string? Phone { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string? Service { get; set; }
        
        public string? Message { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
