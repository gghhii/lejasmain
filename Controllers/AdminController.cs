using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrAshrafMellouli.Models;

namespace DrAshrafMellouli.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Helper: saves an uploaded file to /wwwroot/uploads/ and returns its URL
        public async Task<string?> SaveUploadedFile(IFormFile? file)
        {
            if (file == null || file.Length == 0) return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext)) return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsFolder, uniqueName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{uniqueName}";
        }

        public IActionResult Login() => View();
        
        [HttpPost]
        public IActionResult Login(string password)
        {
            // Simple hardcoded password for demonstration
            if (password == "Admin123!") 
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Accès refusé. Mot de passe incorrect.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            return RedirectToAction("Index", "Home");
        }

        private bool IsAuthenticated() => HttpContext.Session.GetString("IsAdmin") == "true";

        public async Task<IActionResult> Index()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");

            ViewBag.TreatmentsCount = await _context.Treatments.CountAsync();
            ViewBag.ResultsCount = await _context.Results.CountAsync();
            ViewBag.ArticlesCount = await _context.Articles.CountAsync();
            ViewBag.TestimonialsCount = await _context.Testimonials.CountAsync();
            return View();
        }

        #region Treatment Management
        public async Task<IActionResult> Treatments()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View(await _context.Treatments.ToListAsync());
        }

        public IActionResult CreateTreatment() 
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTreatment(Treatment treatment, IFormFile? imageFile)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            // ... (rest of the action)
            ModelState.Remove("ImageUrl");
            if (ModelState.IsValid)
            {
                treatment.ImageUrl = await SaveUploadedFile(imageFile);
                _context.Add(treatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Treatments));
            }
            return View(treatment);
        }

        public async Task<IActionResult> EditTreatment(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (id == null) return NotFound();
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null) return NotFound();
            return View(treatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTreatment(int id, Treatment treatment, IFormFile? imageFile)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (id != treatment.Id) return NotFound();
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                try
                {
                    var newImageUrl = await SaveUploadedFile(imageFile);
                    if (newImageUrl != null)
                        treatment.ImageUrl = newImageUrl;

                    _context.Update(treatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(treatment.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Treatments));
            }
            return View(treatment);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTreatment(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment != null)
            {
                _context.Treatments.Remove(treatment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Treatments));
        }

        private bool TreatmentExists(int id) => _context.Treatments.Any(e => e.Id == id);
        #endregion

        #region Result Management
        public async Task<IActionResult> Results()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View(await _context.Results.ToListAsync());
        }

        public IActionResult CreateResult() 
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResult(Result result, IFormFile? beforeImageFile, IFormFile? afterImageFile)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            ModelState.Remove("BeforeImageUrl");
            ModelState.Remove("AfterImageUrl");

            if (ModelState.IsValid)
            {
                result.BeforeImageUrl = await SaveUploadedFile(beforeImageFile);
                result.AfterImageUrl = await SaveUploadedFile(afterImageFile);
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Results));
            }
            return View(result);
        }

        public async Task<IActionResult> EditResult(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (id == null) return NotFound();
            var result = await _context.Results.FindAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditResult(int id, Result result, IFormFile? beforeImageFile, IFormFile? afterImageFile)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (id != result.Id) return NotFound();
            ModelState.Remove("BeforeImageUrl");
            ModelState.Remove("AfterImageUrl");

            if (ModelState.IsValid)
            {
                try
                {
                    var newBefore = await SaveUploadedFile(beforeImageFile);
                    if (newBefore != null) result.BeforeImageUrl = newBefore;

                    var newAfter = await SaveUploadedFile(afterImageFile);
                    if (newAfter != null) result.AfterImageUrl = newAfter;

                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Results));
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteResult(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var result = await _context.Results.FindAsync(id);
            if (result != null)
            {
                _context.Results.Remove(result);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Results));
        }

        private bool ResultExists(int id) => _context.Results.Any(e => e.Id == id);
        #endregion

        #region Article Management
        public async Task<IActionResult> Articles()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View(await _context.Articles.ToListAsync());
        }

        public IActionResult CreateArticle() 
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(Article article, IFormFile? imageFile)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            ModelState.Remove("CoverImageUrl");

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                    article.CoverImageUrl = await SaveUploadedFile(imageFile);
                
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Articles));
            }
            return View(article);
        }

        public async Task<IActionResult> EditArticle(int? id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (id == null) return NotFound();
            var article = await _context.Articles.FindAsync(id);
            if (article == null) return NotFound();
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(int id, Article article, IFormFile? imageFile)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (id != article.Id) return NotFound();
            ModelState.Remove("CoverImageUrl");

            if (ModelState.IsValid)
            {
                try
                {
                    var newImage = await SaveUploadedFile(imageFile);
                    if (newImage != null) article.CoverImageUrl = newImage;

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Articles));
            }
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Articles));
        }

        private bool ArticleExists(int id) => _context.Articles.Any(e => e.Id == id);
        #endregion

        #region Testimonials
        public async Task<IActionResult> ManageTestimonials()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var testimonials = await _context.Testimonials.ToListAsync();
            return View(testimonials);
        }

        public IActionResult CreateTestimonial() 
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(Testimonial testimonial)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                _context.Testimonials.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageTestimonials));
            }
            return View(testimonial);
        }

        public async Task<IActionResult> EditTestimonial(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null) return NotFound();
            return View(testimonial);
        }

        [HttpPost]
        public async Task<IActionResult> EditTestimonial(Testimonial testimonial)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            if (ModelState.IsValid)
            {
                _context.Testimonials.Update(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageTestimonials));
            }
            return View(testimonial);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageTestimonials));
        }
        #endregion

        #region Appointments Management
        public async Task<IActionResult> Appointments()
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var appointments = await _context.Appointments.OrderByDescending(a => a.CreatedAt).ToListAsync();
            return View(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (!IsAuthenticated()) return RedirectToAction("Login");
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Appointments));
        }
        #endregion
    }
}
