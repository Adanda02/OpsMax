using Microsoft.AspNetCore.Mvc;
using OpsMax.DTO.ViewModels;
using OpsMax.Services.Interfaces;
using System.Threading.Tasks;

namespace OpsMax.Modules.Payments.Controllers
{
    [Route("paymentsource")]
    public class PaymentSourceController : Controller
    {
        private readonly IPaymentSourceService _service;

        public PaymentSourceController(IPaymentSourceService service)
        {
            _service = service;
        }

        // GET: /paymentsource/index
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var allSources = await _service.GetAllAsync();
            return View(allSources);
        }

        // GET: /paymentsource/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /paymentsource/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentSourceCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var id = await _service.CreateAsync(vm, User.Identity.Name);
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /paymentsource/details/5
        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _service.GetDetailsAsync(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }
    }
}
