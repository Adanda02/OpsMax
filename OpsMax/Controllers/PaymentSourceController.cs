using Microsoft.AspNetCore.Mvc;
using OpsMax.ViewModels;
using OpsMax.Services.Interfaces;
using OpsMax.ViewModels;
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

        // -----------------------------
        // GET: /paymentsource/index
        // -----------------------------
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            var allSources = await _service.GetAllAsync();
            return View(allSources);
        }

        // -----------------------------
        // GET: /paymentsource/create
        // -----------------------------
        [HttpGet("create")]
        public IActionResult Create()
        {
            // Use strongly-typed Page VM instead of ViewBag
            var vm = new PaymentSourceCreatePageVM
            {
                Form = new PaymentSourceCreateVM(),
                Suppliers = _service.GetSuppliers(),
                GlAccounts = _service.GetGLAccounts()
            };

            return View(vm);
        }

        // -----------------------------
        // POST: /paymentsource/create
        // -----------------------------
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentSourceCreatePageVM pageVm)
        {
            if (!ModelState.IsValid)
            {
                // Reload suppliers/GL accounts for redisplay
                pageVm.Suppliers = _service.GetSuppliers();
                pageVm.GlAccounts = _service.GetGLAccounts();
                return View(pageVm);
            }

            var id = await _service.CreateAsync(pageVm.Form, User.Identity?.Name);
            return RedirectToAction(nameof(Details), new { id });
        }

        // -----------------------------
        // GET: /paymentsource/details/{id}
        // -----------------------------
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
