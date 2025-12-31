using OpsMax.DTO.ViewModels;
using OpsMax.Models;
using OpsMax.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.Controllers
{
    [Route("[controller]")]
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly IInvoiceService _invoiceService;
        private readonly IWebHostEnvironment _env;
        private object newId;
        public const string UserName = "SYSTEM";

        public CollectionController(
            ICollectionService collectionService,
            IInvoiceService invoiceService,
            IWebHostEnvironment env)
        {
            _collectionService = collectionService;
            _invoiceService = invoiceService;
            _env = env;
        }

        // =========================
        // INDEX
        // =========================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var collections = await _collectionService.GetCollectionsAsync();
            return View(collections);
        }

        // =========================
        // DETAILS
        // =========================
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _collectionService.GetCollectionByIdAsync(id);
            if (entity == null) return NotFound();
            return View(entity);
        }

        // =========================
        // CREATE – EMPTY FORM
        // =========================
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new CollectionCreateViewModel());
        }

        // =========================
        // SEARCH INVOICE
        // =========================
        [HttpPost("SearchInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchInvoice(string invoiceNumber)
        {
            invoiceNumber = invoiceNumber?.Trim();

            if (string.IsNullOrWhiteSpace(invoiceNumber))
            {
                ModelState.AddModelError("", "Invoice number is required.");
                return View("Create", new CollectionCreateViewModel());
            }

            // 🔒 Check latest collection for this invoice
            CollectionEntity? existing =
                await _collectionService.GetLatestByInvoiceAsync(invoiceNumber);

            if (existing != null && existing.OrderStatusID == 3)
            {
                TempData["LockMessage"] =
                    "This invoice has already been fully collected and is now read-only.";

                var lockedVm = new CollectionCreateViewModel
                {
                    InvoiceNumberID = existing.InvoiceNumberID,
                    InvoiceNumber = existing.InvoiceNumber,
                    CustomerID = existing.CustomerID,
                    CustomerName = existing.CustomerName,
                    InvoiceDate = existing.InvoiceDate,
                    Driver = existing.Driver,
                    PhoneNumber = existing.PhoneNumber,
                    VehicleReg = existing.VehicleReg,
                    UserName = UserName,
                    Lines = existing.Lines.Select(l => new CollectionLineViewModel
                    {
                        ItemCodeID = l.ItemCodeID,
                        ItemCode = l.ItemCode,
                        ItemDescription = l.ItemDescription,
                        Warehouse = l.Warehouse,
                        QtyPurchased = l.QtyPurchased,
                        QtyCollected = l.QtyCollected,
                        UnitPrice = l.UnitPrice,
                        LineTotal = l.LineTotal,
                        OrderBalance = l.OrderBalance
                    }).ToList()
                };

                return View("Create", lockedVm);
            }

            // 🟢 Fresh or partially collected invoice
            var invoiceLines = await _invoiceService.GetInvoiceLinesAsync(invoiceNumber);

            if (!invoiceLines.Any())
            {
                ModelState.AddModelError("", "Invoice not found.");
                return View("Create", new CollectionCreateViewModel());
            }

            var header = invoiceLines.First();

            var freshVm = new CollectionCreateViewModel
            {
                InvoiceNumberID = header.InvoiceNumberID,
                InvoiceNumber = header.InvoiceNumber,
                CustomerID = header.CustomerID,
                CustomerName = header.CustomerName,
                InvoiceDate = header.InvoiceDate,
                UserName = UserName,
                Lines = invoiceLines.Select(l => new CollectionLineViewModel
                {
                    ItemCodeID = l.ItemCodeID,
                    ItemCode = l.ItemCode,
                    ItemDescription = l.ItemDescription,
                    Warehouse = l.Warehouse,
                    QtyPurchased = (decimal)l.QtyPurchased,
                    QtyCollected = 0,
                    UnitPrice = l.UnitPrice,
                    LineTotal = l.LineTotal,
                    OrderBalance = (decimal)l.QtyPurchased
                }).ToList()
            };

            return View("Create", freshVm);
        }

        // =========================
        // SAVE COLLECTION (WITH FILE UPLOAD)
        // =========================
        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CollectionCreateViewModel vm)
        {
            // 📎 Handle attachment upload
            if (vm.Attachment != null && vm.Attachment.Length > 0)
            {
                var uploadsRoot = Path.Combine(_env.WebRootPath, "collectiondocs");
                Directory.CreateDirectory(uploadsRoot);

                var fileName =
                    $"{vm.InvoiceNumber}_{DateTime.Now:yyyyMMddHHmmss}" +
                    Path.GetExtension(vm.Attachment.FileName);

                var fullPath = Path.Combine(uploadsRoot, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await vm.Attachment.CopyToAsync(stream);
                }

                vm.AttachmentPath = "/collectiondocs/" + fileName;
            }

            //await _collectionService.SaveCollectionAsync(vm, UserName);
            //return RedirectToAction("Details", new { id = newId });

            int newid = await _collectionService.SaveCollectionAsync(vm, UserName);
            return RedirectToAction("Details", new { id = newid });

        }

        // =========================
        // EDIT
        // =========================
        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (await _collectionService.IsLockedAsync(id))
                return RedirectToAction("Details", new { id });

            var entity = await _collectionService.GetCollectionByIdAsync(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CollectionEntity model)
        {
            if (await _collectionService.IsLockedAsync(id))
                return RedirectToAction("Details", new { id });

            if (id != model.idOrderCollected)
                return BadRequest();

            await _collectionService.UpdateCollectionAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE
        // =========================
        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _collectionService.IsLockedAsync(id))
                return RedirectToAction("Details", new { id });

            var entity = await _collectionService.GetCollectionByIdAsync(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        [HttpPost("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _collectionService.IsLockedAsync(id))
                return RedirectToAction("Details", new { id });

            await _collectionService.DeleteCollectionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
