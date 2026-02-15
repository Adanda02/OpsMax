using Microsoft.AspNetCore.Mvc.Rendering;
using OpsMax.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpsMax.Data;

namespace OpsMax.ViewModels
{
    // =========================================
    // ViewModel for creating a Load
    // =========================================
    public class LoadCreateViewModel
    {
        // Main Load entity
        public Load Load { get; set; }

        // Dropdowns for selection
        public IEnumerable<SelectListItem> Vendors { get; set; }     // From Vendor table
        public IEnumerable<SelectListItem> StockItems { get; set; }  // From StockItem table
        public IEnumerable<SelectListItem> Trucks { get; set; }      // From Truck table
        public IEnumerable<SelectListItem> Drivers { get; set; }     // From Driver table
    }

    // =========================================
    // Extension/helper class for populating dropdowns
    // =========================================
    public static class LoadViewModelExtensions
    {
        public static async Task PopulateDropdownsAsync(this LoadCreateViewModel vm,
                                                        ZimMealDbContext _zimContext,
                                                        ApplicationDbContext _context)
        {
            // -----------------------------
            // Vendors (from ZimMeal/Sage)
            // -----------------------------
            vm.Vendors = await _zimContext.Vendors
                .OrderBy(v => v.Name)
                .Select(v => new SelectListItem
                {
                    Value = v.DCLink.ToString(),
                    Text = $"{v.Account} - {v.Name}"
                })
                .ToListAsync();

            // -----------------------------
            // Stock Items (from ZimMeal/Sage)
            // -----------------------------
            vm.StockItems = await _zimContext.StockItems
                .OrderBy(s => s.Description_1)
                .Select(s => new SelectListItem
                {
                    Value = s.StockLink.ToString(),
                    Text = $"{s.Code} - {s.Description_1}"
                })
                .ToListAsync();

            // -----------------------------
            // Trucks (from main app DB)
            // -----------------------------
            vm.Trucks = await _context.Trucks
                .Where(t => t.Status == "Active")
                .OrderBy(t => t.RegistrationNumber)
                .Select(t => new SelectListItem
                {
                    Value = t.idTruck.ToString(),
                    Text = t.RegistrationNumber
                })
                .ToListAsync();

            // -----------------------------
            // Drivers (from main app DB)
            // -----------------------------
            vm.Drivers = await _context.Drivers
                .Where(d => d.Status == "Active")
                .OrderBy(d => d.FullName)
                .Select(d => new SelectListItem
                {
                    Value = d.idDrivers.ToString(),
                    Text = d.FullName
                })
                .ToListAsync();
        }
    }
}
