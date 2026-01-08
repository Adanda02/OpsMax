using Microsoft.EntityFrameworkCore;
using OpsMax.Data;
using OpsMax.Services;
using OpsMax.Services.Interfaces;
using OpsMax.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// 1. ADD SERVICES
// ----------------------------------------------------

// MVC + Views
builder.Services.AddControllersWithViews();

// Razor runtime compilation
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// ----------------------------------------------------
// 2. DATABASE CONTEXTS
// ----------------------------------------------------

// Main Application DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// External ZIM MEAL / Sage DB
builder.Services.AddDbContext<ZimMealDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ZIMMEALConnection")));

// ----------------------------------------------------
// 3. BUSINESS / DOMAIN SERVICES
// ----------------------------------------------------

builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPaymentSourceService, PaymentSourceService>();

// ----------------------------------------------------
// 4. BUILD APP
// ----------------------------------------------------
var app = builder.Build();

// ----------------------------------------------------
// 5. MIDDLEWARE PIPELINE
// ----------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// ----------------------------------------------------
// 6. ROUTING
// ----------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
