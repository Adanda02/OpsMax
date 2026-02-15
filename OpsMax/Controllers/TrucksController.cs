using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;

namespace OpsMax.Controllers
{
    public class TrucksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrucksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Trucks.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Truck model)
        {
            if (!ModelState.IsValid)
            {
                // DEBUG: see validation errors
                var errors = ModelState.Values
                               .SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();

                return View(model);
            }

            _context.Trucks.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
