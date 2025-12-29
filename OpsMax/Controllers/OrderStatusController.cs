using OpsMax.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OpsMax.Controllers
{
    public class OrderStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatuses.ToListAsync());
        }

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var orderStatus = await _context.OrderStatuses
                .FirstOrDefaultAsync(m => m.idStatus == id);

            if (orderStatus == null)
                return NotFound();

            return View(orderStatus);
        }

        // Add Create/Edit/Delete methods as needed
    }
}
