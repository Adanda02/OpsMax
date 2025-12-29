using Microsoft.EntityFrameworkCore;
using BOLMS.Data;
using BOLMS.Services.Interfaces;
using BOLMS.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// 1. ADD SERVICES
// ----------------------------------------------------

// MVC + Views
builder.Services.AddControllersWithViews();

// Razor runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// ----------------------------------------------------
// 2. DATABASE CONTEXTS
// ----------------------------------------------------

// Default Identity / Admin DB (optional for your system)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// ZIM MEAL external Sage/Invoice database
builder.Services.AddDbContext<ZimMealDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZIMMEALConnection")));

// ----------------------------------------------------
// 3. BUSINESS SERVICES
// ----------------------------------------------------
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();


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

// Default MVC route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
