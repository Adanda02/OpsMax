using OpsMax.Models;
using OpsMax.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpsMax.Services
{
    public interface ILoadService
    {
        Task<IEnumerable<Load>> GetAllLoads();
        Task<Load> GetLoadById(int id);
        Task<LoadViewModel> GetCreateViewModel();
        Task CreateLoad(LoadViewModel model);
    }
}
