using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrAshrafMellouli.Models;

namespace DrAshrafMellouli.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = new HomeViewModel
        {
            Results = await _context.Results.OrderByDescending(r => r.Id).ToListAsync(),
            Articles = await _context.Articles.OrderByDescending(a => a.DatePublished).Take(3).ToListAsync()
        };
        return View(data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public async Task<IActionResult> Face()
    {
        var treatments = await _context.Treatments
            .Where(t => t.Category == Category.Face)
            .ToListAsync();
        return View(treatments);
    }

    public async Task<IActionResult> Hair()
    {
        var treatments = await _context.Treatments
            .Where(t => t.Category == Category.Hair)
            .ToListAsync();
        return View(treatments);
    }

    public async Task<IActionResult> Body()
    {
        var treatments = await _context.Treatments
            .Where(t => t.Category == Category.Body)
            .ToListAsync();
        return View(treatments);
    }

    public async Task<IActionResult> Laser()
    {
        var treatments = await _context.Treatments
            .Where(t => t.Category == Category.Laser)
            .ToListAsync();
        return View(treatments);
    }

    public async Task<IActionResult> Results()
    {
        var results = await _context.Results.OrderByDescending(r => r.Id).ToListAsync();
        return View(results);
    }

    public async Task<IActionResult> Testimonials()
    {
        var testimonials = await _context.Testimonials.ToListAsync();
        return View(testimonials);
    }

    public IActionResult Contact()
    {
        return View();
    }

    public async Task<IActionResult> Journal()
    {
        var articles = await _context.Articles
            .OrderByDescending(a => a.DatePublished)
            .ToListAsync();
        return View(articles);
    }

    public async Task<IActionResult> Treatment(int id)
    {
        var treatment = await _context.Treatments.FindAsync(id);
        
        if (treatment == null)
        {
            // Fallback for demo if ID not found in database
            return RedirectToAction("Face");
        }

        // Fetch other treatments in same category as "Related"
        ViewBag.RelatedTreatments = await _context.Treatments
            .Where(t => t.Category == treatment.Category && t.Id != treatment.Id)
            .Take(3)
            .ToListAsync();

        return View(treatment);
    }

    public async Task<IActionResult> Article(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null)
        {
            // If no articles exist at all, return the static view or handle gracefully
            // For now, if ID is 0 or not found, try to find the latest one
            article = await _context.Articles.OrderByDescending(a => a.DatePublished).FirstOrDefaultAsync();
        }
        
        if (article == null) return View("EmptyArticle"); // Or handle empty state

        // Also fetch related articles
        ViewBag.RelatedArticles = await _context.Articles
            .Where(a => a.Id != article.Id)
            .OrderByDescending(a => a.DatePublished)
            .Take(3)
            .ToListAsync();

        return View(article);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAppointment([FromForm] Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
