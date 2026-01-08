using OpsMax.DTO.ViewModels;
using OpsMax.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsMax.Services.Interfaces
{
    public interface IPaymentSourceService
    {
        /// <summary>
        /// Create a new Payment Source
        /// </summary>
        /// <param name="vm">Payment source create model</param>
        /// <param name="username">User performing the action</param>
        /// <returns>Id of the created Payment Source</returns>
        Task<int> CreateAsync(PaymentSourceCreateVM vm, string username);

        /// <summary>
        /// Get details of a Payment Source including documents
        /// </summary>
        /// <param name="id">Payment Source Id</param>
        /// <returns>PaymentSourceDetailsVM or null if not found</returns>
        Task<PaymentSourceDetailsVM> GetDetailsAsync(int id);

        /// <summary>
        /// Get all Payment Sources
        /// </summary>
        /// <returns>List of PaymentSource</returns>
        Task<List<PaymentSource>> GetAllAsync();
    }
}
