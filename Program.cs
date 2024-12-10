using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.Extensions.Hosting;
using WebClothes.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebClothes.Irepository;
using WebClothes.Repository;
using WebClothes.Services.Vnpay;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<ShopContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization(options => 
{ options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin")); 
  options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User")); });
builder.Services.AddScoped<IProUnitOfWork, ProUnitOfWork>();
builder.Services.AddScoped<IOrderUnitOfWork,OrderUnitOfWork>();
builder.Services.AddScoped<IVnPayService,VnPayService>();
var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "/{Area=User}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin", 
        pattern: "/Admin/{controller=Home}/{action=Index}/{id?}"); 
    endpoints.MapAreaControllerRoute(
        name: "User", 
        areaName: "User", 
        pattern: "/User/{controller=Home}/{action=Index}/{id?}");
});
app.Run();



//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<ShopContext>();
//        DataInit.Init(context);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred creating the DB.");
//    }
//}
