using OpsMax.Models;
using OpsMax.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsMax.Services.Interfaces
{
    public interface IPaymentSourceService
    {
        // =====================================================
        // CREATE
        // =====================================================
        /// <summary>
        /// Create a new Payment Source
        /// </summary>
        /// <param name="vm">Payment source create model</param>
        /// <param name="username">User performing the action</param>
        /// <returns>Id of the created Payment Source</returns>
        Task<int> CreateAsync(PaymentSourceCreateVM vm, string username);

        // =====================================================
        // DETAILS
        // =====================================================
        /// <summary>
        /// Get details of a Payment Source including documents
        /// </summary>
        /// <param name="id">Payment Source Id</param>
        /// <returns>PaymentSourceDetailsVM or null if not found</returns>
        Task<PaymentSourceDetailsVM> GetDetailsAsync(int id);

        // =====================================================
        // LIST
        // =====================================================
        /// <summary>
        /// Get all Payment Sources
        /// </summary>
        /// <returns>List of PaymentSource</returns>
        Task<List<PaymentSource>> GetAllAsync();

        // =====================================================
        // SAGE LOOKUPS (KEYLESS DTOs)
        // =====================================================
        /// <summary>
        /// Get Suppliers with GRVs from Sage
        /// </summary>
        /// <returns>List of SupplierGRVVM</returns>
        List<SupplierGRVVM> GetSuppliers();

        /// <summary>
        /// Get GL Accounts from Sage
        /// </summary>
        /// <returns>List of GLAccountVM</returns>
        List<GLAccountVM> GetGLAccounts();
    }
}
