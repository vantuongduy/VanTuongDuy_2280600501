using Microsoft.EntityFrameworkCore;
using VanTuongDuy_2280600501.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()   // Cho phép t?t c? các ngu?n truy c?p
               .AllowAnyMethod()   // Cho phép t?t c? các ph??ng th?c HTTP (GET, POST, PUT, DELETE, ...)
               .AllowAnyHeader()); // Cho phép t?t c? các header
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseCors("AllowAll");

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
