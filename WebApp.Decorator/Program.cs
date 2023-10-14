using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Decorator.Context;
using WebApp.Decorator.Models;
using WebApp.Decorator.Repositories;
using WebApp.Decorator.Repositories.Decorator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Mssql"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

    var context = sp.GetRequiredService<AppDbContext>();
    var memoryCache = sp.GetRequiredService<IMemoryCache>();
    var productRepository = new ProductRepository(context);
    var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();

    if (httpContextAccessor.HttpContext.User.Identity.Name == "user1")
    {
        var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
        return cacheDecorator;
    }

    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);

    return logDecorator;
});

var app = builder.Build();

using (var scope= app.Services.CreateScope()){

    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
dbContext.Database.Migrate();

if (!userManager.Users.Any())
{
                                                                                            //passwordda hassasiyete dikkat
    userManager.CreateAsync(new AppUser(){ UserName = "levent",Email = "levent@gmail.com"}, "P123s.").Wait();
    userManager.CreateAsync(new AppUser(){ UserName = "kalkavan",Email = "kalkavan@hotmail.com"}, "P123s.").Wait();
    userManager.CreateAsync(new AppUser(){ UserName = "marmaris",Email = "marmaris@hotmail.com"}, "P123s.").Wait();
    userManager.CreateAsync(new AppUser(){ UserName = "test",Email = "test@outlook.com"}, "P123s.").Wait();
}

}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();