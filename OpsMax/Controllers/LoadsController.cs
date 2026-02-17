using Microsoft.AspNetCore.Mvc;
using OpsMax.Services;
using System.Threading.Tasks;

namespace OpsMax.Controllers
{
    public class LoadsController : Controller
    {
        private readonly ILoadService _loadService;

        public LoadsController(ILoadService loadService)
        {
            _loadService = loadService;
        }

        public async Task<IActionResult> Index()
        {
            var loads = await _loadService.GetAllLoads();
            return View(loads);
        }

        public async Task<IActionResult> Create()
        {
            var vm = await _loadService.GetCreateViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OpsMax.ViewModels.LoadViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _loadService.CreateLoad(model);
                return RedirectToAction(nameof(Index));
            }

            return View(await _loadService.GetCreateViewModel());
        }
    }
}
