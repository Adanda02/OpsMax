using Microsoft.AspNetCore.Mvc;
using OpsMax.Data;
using OpsMax.Enums;
using OpsMax.Models;

namespace OpsMax.Controllers
{
    public class LoadDocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public LoadDocumentsController(
            ApplicationDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(
            int loadId,
            DocumentType type,
            IFormFile file)
        {
            var folder = Path.Combine(
                _env.WebRootPath, "uploads");

            Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var doc = new LoadDocument
            {
                LoadID = loadId,
                DocumentType = type,
                FilePath = filePath,
                UploadedBy = User.Identity.Name
            };

            _context.Add(doc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Loads", new { id = loadId });
        }
    }

}
