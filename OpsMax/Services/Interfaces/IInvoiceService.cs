using OpsMax.DTO;

namespace OpsMax.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceLineDto>> GetInvoiceLinesAsync(string invoiceNumber);
    }
}
