using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpsMax.ViewModels
{
    // =========================================
    // ViewModel for Creating / Editing a Load
    // =========================================
    public class LoadCreateViewModel
    {
        // Main Load entity

            public Load Load { get; set; } = new Load();

            public List<SelectListItem> Vendors { get; set; } = new();
            public List<SelectListItem> StockItems { get; set; } = new();
            public List<SelectListItem> Trucks { get; set; } = new();
            public List<SelectListItem> Drivers { get; set; } = new();
        
    }

    // =========================================
    // Extension Helper for Populating Dropdowns
    // =========================================
    public static class LoadViewModelExtensions
    {
        public static async Task PopulateDropdownsAsync(
            this LoadCreateViewModel vm,
            ZimMealDbContext zimContext,
            ApplicationDbContext context)
        {
            // Vendors from ZimMeal
            vm.Vendors = await zimContext.Vendors
                .OrderBy(v => v.Name)
                .Select(v => new SelectListItem
                {
                    Value = v.DCLink.ToString(),
                    Text = v.Account + " - " + v.Name
                })
                .ToListAsync();

            // Stock Items from ZimMeal
            vm.StockItems = await zimContext.StockItems
                .OrderBy(s => s.Description_1)
                .Select(s => new SelectListItem
                {
                    Value = s.StockLink.ToString(),
                    Text = s.Code + " - " + s.Description_1
                })
                .ToListAsync();

            // Trucks from OpsMax
            vm.Trucks = await context.Trucks
                .Where(t => t.Status == "Active")
                .OrderBy(t => t.RegistrationNumber)
                .Select(t => new SelectListItem
                {
                    Value = t.idTruck.ToString(),
                    Text = t.RegistrationNumber
                })
                .ToListAsync();

            // Drivers from OpsMax
            vm.Drivers = await context.Drivers
                .Where(d => d.Status == "Active")
                .OrderBy(d => d.FullName)
                .Select(d => new SelectListItem
                {
                    Value = d.idDriver.ToString(),
                    Text = d.FullName
                })
                .ToListAsync();
        }
    }

}

