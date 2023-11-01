using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MvcProje.Models;
using MvcProje.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UygulamaDbContext>(Options=>
Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// dikkat yeni bir repository sýnýfý olusturdugunuzda burada servislere eklemelisiniz
// kitaptururepository dosyasýnýn olusturulmasýný saglar..==depencendy Injection
builder.Services.AddScoped<IKitapTuruRepository, KitapTuruRepository>();

// 
builder.Services.AddScoped<IKitapRepository, KitapRepository>();
builder.Services.AddScoped<IKiralamaRepository, KiralamaRepository>();


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
