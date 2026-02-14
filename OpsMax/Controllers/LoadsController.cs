using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using OpsMax.ViewModels;

namespace OpsMax.Controllers
{
    public class LoadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // INDEX
        // =====================================================
        public async Task<IActionResult> Index()
        {
            var loads = await _context.Loads
                .Include(l => l.Truck)
                .Include(l => l.Driver)
                .ToListAsync();

            return View(loads);
        }

        // =====================================================
        // CREATE (GET)
        // =====================================================
        public IActionResult Create()
        {
            var viewModel = new LoadCreateViewModel
            {
                Load = new Load(),

                Trucks = _context.Trucks
                    .Select(t => new SelectListItem
                    {
                        Value = t.idTruck.ToString(),
                        Text = t.RegistrationNumber
                    }).ToList(),

                Drivers = _context.Drivers
                    .Select(d => new SelectListItem
                    {
                        Value = d.idDrivers.ToString(),
                        Text = d.FullName
                    }).ToList()
            };

            return View(viewModel);
        }

        // =====================================================
        // CREATE (POST)
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoadCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate dropdowns if validation fails
                viewModel.Trucks = _context.Trucks
                    .Select(t => new SelectListItem
                    {
                        Value = t.idTruck.ToString(),
                        Text = t.RegistrationNumber
                    }).ToList();

                viewModel.Drivers = _context.Drivers
                    .Select(d => new SelectListItem
                    {
                        Value = d.idDrivers.ToString(),
                        Text = d.FullName
                    }).ToList();

                return View(viewModel);
            }

            var load = viewModel.Load;

            // Calculate shortage
            load.ShortageQuantity = load.LoadedQuantity - load.ActualQuantity;

            // Audit
            load.CreatedBy = User?.Identity?.Name ?? "System";

            _context.Loads.Add(load);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
