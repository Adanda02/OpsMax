using OpsMax.Data;
using OpsMax.DTO.ViewModels;
using OpsMax.Models;
using OpsMax.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.Services.Implementations
{
    public class PaymentSourceService : IPaymentSourceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PaymentSourceService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        /// <summary>
        /// Create a new Payment Source with uploaded documents
        /// </summary>
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

            _context.PaymentSources.Add(source);
            await _context.SaveChangesAsync();

            // Ensure upload folder exists
            var uploadPath = Path.Combine(_env.WebRootPath, "sourcedocs");
            Directory.CreateDirectory(uploadPath);

            // Save each file
            for (int i = 0; i < vm.Files.Count; i++)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.Files[i].FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                using var fs = new FileStream(filePath, FileMode.Create);
                await vm.Files[i].CopyToAsync(fs);

                _context.PaymentSourceDocuments.Add(new PaymentSourceDocument
                {
                    PaymentSourceID = source.idPaymentSource,
                    DocumentType = vm.DocumentTypes.ElementAtOrDefault(i) ?? "Unknown",
                    FilePath = fileName,
                    UploadedBy = user,
                    UploadedDate = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            return source.idPaymentSource;
        }

        /// <summary>
        /// Get details of a Payment Source including documents
        /// </summary>
        public async Task<PaymentSourceDetailsVM> GetDetailsAsync(int id)
        {
            var header = await _context.PaymentSources
                .FirstOrDefaultAsync(x => x.idPaymentSource == id);

            if (header == null) return null;

            var documents = await _context.PaymentSourceDocuments
                .Where(d => d.PaymentSourceID == id)
                .ToListAsync();

            return new PaymentSourceDetailsVM
            {
                Header = header,
                Documents = documents
            };
        }

        /// <summary>
        /// Get all Payment Sources ordered by DateCaptured descending
        /// </summary>
        public async Task<List<PaymentSource>> GetAllAsync()
        {
            return await _context.PaymentSources
                .OrderByDescending(x => x.DateCaptured)
                .ToListAsync();
        }
    }
}
