using Microsoft.AspNetCore.Mvc;
using OpsMax.Data;
using OpsMax.Models;

namespace OpsMax.Controllers
{
    public class CustomerAllocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerAllocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int loadId)
        {
            ViewBag.LoadID = loadId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerAllocation model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Loads");
            }

            return View(model);
        }
    }

}
