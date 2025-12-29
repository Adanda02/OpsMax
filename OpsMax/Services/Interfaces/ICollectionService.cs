using OpsMax.DTO.ViewModels;
using OpsMax.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsMax.Services.Interfaces
{
    public interface ICollectionService
    {
        // =========================
        // CREATE
        // =========================
        Task SaveCollectionAsync(CollectionCreateViewModel vm, string userName);
        Task SaveCollectionAsync(CollectionSaveViewModel vm, string userName);

        // =========================
        // READ
        // =========================
        Task<List<CollectionEntity>> GetCollectionsAsync();
        Task<CollectionEntity?> GetCollectionByIdAsync(int id);
        Task<CollectionEntity?> GetCollectionByInvoiceAsync(string invoiceNumber);
        Task<CollectionEntity?> GetLatestByInvoiceAsync(string invoiceNumber);

        // =========================
        // UPDATE
        // =========================
        Task UpdateCollectionAsync(CollectionEntity collectionEntity);

        // =========================
        // DELETE
        // =========================
        Task DeleteCollectionAsync(int id);

        // =========================
        // LOCK CHECK
        // =========================
        Task<bool> IsLockedAsync(int id);
    }
}
