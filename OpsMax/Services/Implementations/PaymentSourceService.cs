using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using OpsMax.Services.Interfaces;
using OpsMax.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.Services.Implementations
{
    public class PaymentSourceService : IPaymentSourceService
    {
        private readonly ApplicationDbContext _opsContext;
        private readonly ZimMealDbContext _zimContext; // Use ZimMealDbContext instead of SageDbContext
        private readonly IWebHostEnvironment _env;

        public PaymentSourceService(
            ApplicationDbContext opsContext,
            ZimMealDbContext zimContext,
            IWebHostEnvironment env)
        {
            _opsContext = opsContext;
            _zimContext = zimContext;
            _env = env;
        }

        // =====================================================
        // CREATE PAYMENT SOURCE
        // =====================================================
        public async Task<int> CreateAsync(PaymentSourceCreateVM vm, string user)
        {
            var source = new PaymentSource
            {
                SourceType = vm.SourceType,
                AccountID = vm.AccountId,
                OrderReference = vm.OrderReference,
                RaisedBy = user,
                CreatedDate = DateTime.UtcNow,
                DateCaptured = DateTime.UtcNow,
                Status = "Pending"
            };

            _opsContext.PaymentSources.Add(source);
            await _opsContext.SaveChangesAsync();

            // -----------------------------
            // FILE UPLOADS
            // -----------------------------
            var uploadPath = Path.Combine(_env.WebRootPath, "sourcedocs");
            Directory.CreateDirectory(uploadPath);

            for (int i = 0; i < vm.Files.Count; i++)
            {
                var file = vm.Files[i];
                if (file == null || file.Length == 0) continue;

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                using var fs = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fs);

                _opsContext.PaymentSourceDocuments.Add(new PaymentSourceDocument
                {
                    PaymentSourceID = source.idPaymentSource,
                    DocumentType = vm.DocumentTypes.ElementAtOrDefault(i) ?? "Unknown",
                    FilePath = fileName,
                    UploadedBy = user,
                    UploadedDate = DateTime.UtcNow
                });
            }

            await _opsContext.SaveChangesAsync();
            return source.idPaymentSource;
        }

        // =====================================================
        // DETAILS
        // =====================================================
        public async Task<PaymentSourceDetailsVM> GetDetailsAsync(int id)
        {
            var header = await _opsContext.PaymentSources
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.idPaymentSource == id);

            if (header == null) return null;

            var documents = await _opsContext.PaymentSourceDocuments
                .AsNoTracking()
                .Where(d => d.PaymentSourceID == id)
                .ToListAsync();

            return new PaymentSourceDetailsVM
            {
                Header = header,
                Documents = documents
            };
        }

        // =====================================================
        // LIST
        // =====================================================
        public async Task<List<PaymentSource>> GetAllAsync()
        {
            return await _opsContext.PaymentSources
                .AsNoTracking()
                .OrderByDescending(x => x.DateCaptured)
                .ToListAsync();
        }

        // =====================================================
        // ZIMMEAL LOOKUPS (READ-ONLY / KEYLESS)
        // =====================================================

        /// <summary>
        /// Suppliers with GRVs (ZimMeal)
        /// </summary>
        public List<SupplierGRVVM> GetSuppliers()
        {
            return _zimContext.Set<SupplierGRVVM>()
                .FromSqlRaw(@"
                    SELECT 
                        V.DCLink AS SupplierId,
                        V.Name   AS SupplierName,
                        INM.GrvNumber
                    FROM InvNum INM
                    LEFT JOIN Vendor V ON INM.AccountID = V.DCLink
                    WHERE INM.DocType IN (2, 5)
                ")
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// GL Accounts (ZimMeal)
        /// </summary>
        public List<GLAccountVM> GetGLAccounts()
        {
            return _zimContext.Set<GLAccountVM>()
                .FromSqlRaw(@"
                    SELECT
                        AccountLink        AS GlAccountId,
                        Master_Sub_Account AS AccountCode,
                        Description
                    FROM Accounts
                ")
                .AsNoTracking()
                .ToList();
        }
    }
}
