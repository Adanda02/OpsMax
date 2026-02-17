using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models.Views;
using System.Threading.Tasks;

namespace OpsMax.Controllers
{
    [Route("collections-report")]
    public class CollectionsReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectionsReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /collections-report
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var data = await _context.Set<CollectionsSummaryView>()
                .AsNoTracking()
                .OrderByDescending(x => x.DateCollected)
                .ToListAsync();

            return View(data);
        }
    }
}
