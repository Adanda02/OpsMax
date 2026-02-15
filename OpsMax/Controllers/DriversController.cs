using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using System.Threading.Tasks;

namespace OpsMax.Controllers
{
    public class DriversController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DriversController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================================
        // INDEX
        // ================================
        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return View(drivers);
        }

        // ================================
        // DETAILS
        // ================================
        public async Task<IActionResult> Details(int id)
        {
            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.idDriver == id);

            if (driver == null)
                return NotFound();

            return View(driver);
        }

        // ================================
        // CREATE
        // ================================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Driver model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Drivers.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // EDIT
        // ================================
        public async Task<IActionResult> Edit(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Driver model)
        {
            if (id != model.idDriver)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================================
        // DELETE
        // ================================
        public async Task<IActionResult> Delete(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null) return NotFound();

            return View(driver);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
