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
        private readonly ZimMealDbContext _zimContext;

        public LoadsController(
            ApplicationDbContext context,
            ZimMealDbContext zimContext)
        {
            _context = context;
            _zimContext = zimContext;
        }

        // =====================================================
        // INDEX
        // =====================================================
        public async Task<IActionResult> Index()
        {
            var loads = await _context.Loads
                .Include(l => l.Truck)
                .Include(l => l.Driver)
                .OrderByDescending(l => l.CreatedDate)
                .ToListAsync();

            return View(loads);
        }

        // =====================================================
        // DETAILS
        // =====================================================
        public async Task<IActionResult> Details(int id)
        {
            var load = await _context.Loads
                .Include(l => l.Truck)
                .Include(l => l.Driver)
                .FirstOrDefaultAsync(l => l.idLoad == id);

            if (load == null)
                return NotFound();

            return View(load);
        }

        // =====================================================
        // CREATE (GET)
        // =====================================================
        public async Task<IActionResult> Create()
        {
            var viewModel = new LoadCreateViewModel
            {
                Load = new Load
                {
                    LoadDate = DateTime.Today,
                    EstimatedArrivalDate = DateTime.Today.AddDays(1)
                }
            };

            await PopulateDropdowns(viewModel);
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
                await PopulateDropdowns(viewModel);
                return View(viewModel);
            }

            var load = viewModel.Load;

            // Defensive calculation
            load.ShortageQuantity =
                (load.LoadedQuantity) -
                (load.ActualQuantity);

            load.CreatedBy = User?.Identity?.Name ?? "System";
            load.CreatedDate = DateTime.Now;
            load.Status ??= "Loaded";

            _context.Loads.Add(load);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // EDIT (GET)
        // =====================================================
        public async Task<IActionResult> Edit(int id)
        {
            var load = await _context.Loads.FindAsync(id);
            if (load == null)
                return NotFound();

            var vm = new LoadCreateViewModel
            {
                Load = load
            };

            await PopulateDropdowns(vm);
            return View(vm);
        }

        // =====================================================
        // EDIT (POST)
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoadCreateViewModel viewModel)
        {
            if (id != viewModel.Load.idLoad)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(viewModel);
                return View(viewModel);
            }

            viewModel.Load.ShortageQuantity =
                (viewModel.Load.LoadedQuantity) -
                (viewModel.Load.ActualQuantity);

            _context.Update(viewModel.Load);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // DROPDOWN POPULATION (MERGED & CLEAN)
        // =====================================================
        private async Task PopulateDropdowns(LoadCreateViewModel vm)
        {
            // ===============================
            // Vendors (Sage)
            // ===============================
            vm.Vendors = await _zimContext.Vendors
                .OrderBy(v => v.Name)
                .Select(v => new SelectListItem
                {
                    Value = v.DCLink.ToString(),
                    Text = $"{v.Account} - {v.Name}"
                })
                .ToListAsync();

            // ===============================
            // Stock Items (Sage)
            // ===============================
            vm.StockItems = await _zimContext.StockItems
                .OrderBy(s => s.Description_1)
                .Select(s => new SelectListItem
                {
                    Value = s.StockLink.ToString(),
                    Text = $"{s.Code} - {s.Description_1}"
                })
                .ToListAsync();

            // ===============================
            // Trucks (OpsMax)
            // ===============================
            vm.Trucks = await _context.Trucks
                .Where(t => t.Status == "Active")
                .OrderBy(t => t.RegistrationNumber)
                .Select(t => new SelectListItem
                {
                    Value = t.idTruck.ToString(),
                    Text = t.RegistrationNumber
                })
                .ToListAsync();

            // ===============================
            // Drivers (OpsMax)
            // ===============================
            vm.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .OrderBy(d => d.FullName)
                .Select(d => new SelectListItem
                {
                    Value = d.idDrivers.ToString(),
                    Text = d.FullName
                })
                .ToListAsync();
        }
    }
}
