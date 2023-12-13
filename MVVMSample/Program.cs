using MVVMDataLayer;
using MVVMViewModelLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// get connection string from appsettings.json
string cnn = builder.Configuration.GetConnectionString("AdvWorksConnectionString");

// Add AdventureWorks DbContext object
builder.Services.AddDbContext<AdvWorksDbContext>(options => options.UseSqlServer(cnn));

// Add Classes for Scoped DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductViewModel>();

builder.Services.AddProblemDetails();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();