using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using OpsMax.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.Controllers
{
    public class LoadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ========================
        // INDEX
        // ========================
        public async Task<IActionResult> Index()
        {
            var loads = await _context.Loads
                .Include(l => l.Truck)
                .Include(l => l.Driver)
                .Include(l => l.Vendor)
                .ToListAsync();

            return View(loads);
        }

        // ========================
        // CREATE (GET)
        // ========================
        public IActionResult Create()
        {
            var viewModel = new LoadCreateViewModel
            {
                Load = new Load()
            };

            PopulateDropdowns(viewModel);

            return View(viewModel);
        }

        // ========================
        // CREATE (POST)
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoadCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(viewModel);
                return View(viewModel);
            }

            var load = viewModel.Load;
            load.ShortageQuantity = load.LoadedQuantity - load.ActualQuantity;
            load.CreatedBy = User?.Identity?.Name ?? "System";

            _context.Loads.Add(load);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ========================
        // HELPER: Populate dropdowns
        // ========================
        private void PopulateDropdowns(LoadCreateViewModel viewModel)
        {
            viewModel.Vendors = _context.Vendors
                .Select(s => new SelectListItem
                {
                    Value = s.DCLink.ToString(),
                    Text = s.Name
                })
                .ToList();

            viewModel.Trucks = _context.Trucks
                .Select(t => new SelectListItem
                {
                    Value = t.idTruck.ToString(),
                    Text = t.RegistrationNumber
                })
                .ToList();

            viewModel.Drivers = _context.Drivers
                .Select(d => new SelectListItem
                {
                    Value = d.idDrivers.ToString(),
                    Text = d.FullName
                })
                .ToList();
        }
    }
}
