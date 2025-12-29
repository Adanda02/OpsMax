using BOLMS.DTO;

namespace BOLMS.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceLineDto>> GetInvoiceLinesAsync(string invoiceNumber);
    }
}
