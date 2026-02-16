using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using OpsMax.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        // ==========================================
        // GET: Loads
        // ==========================================
        public async Task<IActionResult> Index()
        {
            var loads = await _context.Loads
                .Include(l => l.Truck)
                .Include(l => l.Driver)
                .OrderByDescending(l => l.CreatedDate)
                .ToListAsync();

            return View(loads);
        }

        // ==========================================
        // GET: Loads/Create
        // ==========================================
        public async Task<IActionResult> Create()
        {
            var vm = new LoadCreateViewModel
            {
                Load = new Load
                {
                    LoadDate = DateTime.Today,
                    EstimatedArrivalDate = DateTime.Today.AddDays(1)
                }
            };

            await vm.PopulateDropdownsAsync(_zimContext, _context);

            return View(vm);
        }

        // ==========================================
        // POST: Loads/Create
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoadCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await vm.PopulateDropdownsAsync(_zimContext, _context);
                return View(vm);
            }

            var load = vm.Load;

            if (load.DCLink == 0 || load.StockLink == 0 ||
                load.idTruck == 0 || load.idDriver == 0)
            {
                ModelState.AddModelError("", "All dropdowns must be selected.");
                await vm.PopulateDropdownsAsync(_zimContext, _context);
                return View(vm);
            }

            load.CreatedDate = DateTime.Now;
            load.Status = "Loaded";

            _context.Loads.Add(load);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
