using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;

namespace OpsMax.Controllers
{
    public class LoadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var loads = await _context.Loads
                .Include(l => l.Truck)
                .Include(l => l.Driver)
                .ToListAsync();

            return View(loads);
        }

        public IActionResult Create()
        {
            ViewBag.Trucks = _context.Trucks.ToList();
            ViewBag.Drivers = _context.Drivers.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Load model)
        {
            if (ModelState.IsValid)
            {
                model.ShortageQuantity =
                    model.LoadedQuantity - model.ActualQuantity;

                model.CreatedBy = User.Identity.Name;

                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }

}
