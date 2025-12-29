using OpsMax.Data;
using OpsMax.DTO;
using OpsMax.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsMax.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ZimMealDbContext _context;

        public InvoiceService(ZimMealDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceLineDto>> GetInvoiceLinesAsync(string invoiceNumber)
        {
            if (string.IsNullOrWhiteSpace(invoiceNumber))
                throw new ArgumentException("Invoice number is required.", nameof(invoiceNumber));

            var param = new SqlParameter("@InvoiceNumber", invoiceNumber.Trim());

            return await _context.InvoiceLineDtos
                .FromSqlRaw(
                    "EXEC dbo.usp_GetInvoiceLinesByInvoiceNumber @InvoiceNumber",
                    param)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
