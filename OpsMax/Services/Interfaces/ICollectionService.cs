using OpsMax.Models;
using OpsMax.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsMax.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<int> SaveCollectionAsync(CollectionCreateViewModel vm, string userName);

        Task<List<CollectionEntity>> GetCollectionsAsync();
        Task<CollectionEntity?> GetCollectionByIdAsync(int id);
        Task<CollectionEntity?> GetCollectionByInvoiceAsync(string invoiceNumber);
        Task<CollectionEntity?> GetLatestByInvoiceAsync(string invoiceNumber);
        Task<List<CollectionSummaryVM>> GetCollectionsSummaryAsync();


        Task UpdateCollectionAsync(CollectionEntity entity);
        Task DeleteCollectionAsync(int id);
        Task<bool> IsLockedAsync(int id);
        //Task<string?> GetCollectionsSummaryAsync();
    }
}
