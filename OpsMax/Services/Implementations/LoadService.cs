using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using OpsMax.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.Services
{
    public class LoadService : ILoadService
    {
        private readonly ApplicationDbContext _context;
        private readonly ZimMealDbContext _zimContext;

        public LoadService(ApplicationDbContext context, ZimMealDbContext zimContext)
        {
            _context = context;
            _zimContext = zimContext;
        }

        public async Task<IEnumerable<Load>> GetAllLoads()
        {
            return await _context.Loads.ToListAsync();
        }

        public async Task<Load> GetLoadById(int id)
        {
            return await _context.Loads.FindAsync(id);
        }

        public async Task<LoadViewModel> GetCreateViewModel()
        {
            var vm = new LoadViewModel
            {
                LoadDate = DateTime.Now,
                EstimatedArrivalDate = DateTime.Now.AddDays(1),
                Status = "Loaded",
                Suppliers = await _zimContext.Vendors
                    .Select(static s => new SelectListItem
                    {
                        Value = s.DCLink.ToString(),
                        Text = (string)s.Name
                    }).ToListAsync(),

                Stocks = await _zimContext.StockItems
                    .Select(static s => new SelectListItem
                    {
                        Value = s.StockLink.ToString(),
                        Text = (string)s.Description_1
                    }).ToListAsync(),

                Trucks = await _context.Trucks
                    .Select(static t => new SelectListItem
                    {
                        Value = t.idTruck.ToString(),
                        Text = t.RegistrationNumber
                    }).ToListAsync(),

                Drivers = await _context.Drivers
                    .Select(static d => new SelectListItem
                    {
                        Value = d.idDriver.ToString(),
                        Text = d.FullName
                    }).ToListAsync()
            };

            return vm;
        }

        public async Task CreateLoad(LoadViewModel model)
        {
            var load = new Load
            {
                DCLink = model.DCLink,
                StockLink = model.StockLink,
                idTruck = model.idTruck,
                idDriver = model.idDriver,
                LoadedQuantity = model.LoadedQuantity,
                LoadDate = model.LoadDate,
                EstimatedArrivalDate = model.EstimatedArrivalDate,
                Status = "Loaded",
                CreatedDate = DateTime.Now
            };

            _context.Loads.Add(load);
            await _context.SaveChangesAsync();
        }
    }
}
