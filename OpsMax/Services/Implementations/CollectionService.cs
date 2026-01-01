using OpsMax.Data;
using OpsMax.DTO.ViewModels;
using OpsMax.Models;
using OpsMax.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ApplicationDbContext _context;

        public CollectionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // CREATE / SAVE COLLECTION
        // =========================
        public async Task<int> SaveCollectionAsync(CollectionCreateViewModel vm, string userName)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));

            if (vm.Lines == null || !vm.Lines.Any())
                throw new InvalidOperationException("No collection lines supplied.");

            // 🔒 HARD LOCK – prevent re-collecting completed invoices
            var existing = await _context.Collections
                .Where(x => x.InvoiceNumber == vm.InvoiceNumber)
                .OrderByDescending(x => x.DateCollected)
                .FirstOrDefaultAsync();

            if (existing != null && existing.OrderStatusID == 3)
                throw new InvalidOperationException(
                    "This invoice has already been fully collected and is locked."
                );

            // 🔢 AUTO-CALCULATE STATUS
            var totalPurchased = vm.Lines.Sum(x => x.QtyPurchased);
            var totalCollected = vm.Lines.Sum(x => x.QtyCollected);

            int statusId =
                totalCollected == 0 ? 1 :
                totalCollected < totalPurchased ? 2 :
                totalCollected == totalPurchased ? 3 :
                4;

            // =========================
            // HEADER
            // =========================
            var header = new CollectionEntity
            {
                DateCollected = DateTime.Now,
                InvoiceNumberID = (int)vm.InvoiceNumberID,
                InvoiceNumber = vm.InvoiceNumber,

                CustomerID = vm.CustomerID,
                CustomerName = vm.CustomerName,
                InvoiceDate = vm.InvoiceDate,

                Driver = vm.Driver,
                PhoneNumber = vm.PhoneNumber,
                VehicleReg = vm.VehicleReg,

                OrderStatusID = statusId,
                OrderBalance = vm.Lines.Sum(l => l.OrderBalance),

                AttachmentPath = vm.AttachmentPath,

                UserName = userName,
                DateStamp = DateTime.Now,

                Lines = vm.Lines.Select(l => new CollectionLineEntity
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

            _context.Collections.Add(header);
            await _context.SaveChangesAsync();

            // 🔑 RETURN NEW ID
            return header.idOrderCollected;
        }

        // =========================
        // READ
        // =========================
        public async Task<List<CollectionEntity>> GetCollectionsAsync()
        {
            return await _context.Collections
                .Include(h => h.Lines)
                .AsNoTracking()
                .OrderByDescending(x => x.DateCollected)
                .ToListAsync();
        }

        public async Task<CollectionEntity?> GetCollectionByIdAsync(int id)
        {
            return await _context.Collections
                .Include(h => h.Lines)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.idOrderCollected == id);
        }

        public async Task<CollectionEntity?> GetCollectionByInvoiceAsync(string invoiceNumber)
        {
            return await _context.Collections
                .Include(h => h.Lines)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.InvoiceNumber == invoiceNumber);
        }

        public async Task<CollectionEntity?> GetLatestByInvoiceAsync(string invoiceNumber)
        {
            return await _context.Collections
                .Include(c => c.Lines)
                .Where(c => c.InvoiceNumber == invoiceNumber)
                .OrderByDescending(c => c.DateCollected)
                .FirstOrDefaultAsync();
        }

        // =========================
        // SUMMARY (VIEW)
        // =========================
        public async Task<List<CollectionSummaryVM>> GetCollectionsSummaryAsync()
        {
            return await _context.CollectionsSummary
                .OrderByDescending(x => x.DateCollected)
                .ToListAsync();
        }

        // =========================
        // UPDATE
        // =========================
        public async Task UpdateCollectionAsync(CollectionEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Collections.Update(entity);
            await _context.SaveChangesAsync();
        }

        // =========================
        // DELETE
        // =========================
        public async Task DeleteCollectionAsync(int id)
        {
            var entity = await _context.Collections
                .Include(h => h.Lines)
                .FirstOrDefaultAsync(c => c.idOrderCollected == id);

            if (entity == null)
                throw new InvalidOperationException("Collection not found.");

            _context.CollectionLines.RemoveRange(entity.Lines);
            _context.Collections.Remove(entity);

            await _context.SaveChangesAsync();
        }

        // =========================
        // CHECK IF LOCKED
        // =========================
        public async Task<bool> IsLockedAsync(int id)
        {
            return await _context.Collections
                .AnyAsync(x => x.idOrderCollected == id && x.OrderStatusID == 3);
        }
    }
}
