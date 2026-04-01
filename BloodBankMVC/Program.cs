using BloodBankMVC.Data;
using BloodBankMVC.Service.Implementation;
using BloodBankMVC.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BloodBankContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IRequestorService, RequestorService>();
builder.Services.AddScoped<IBloodInventoryService, BloodInventoryService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IBloodGroupService, BloodGroupService>();

// Add Session support for Admin authentication
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
