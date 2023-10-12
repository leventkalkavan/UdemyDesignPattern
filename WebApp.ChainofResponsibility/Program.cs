using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainofResponsibility.Context;
using WebApp.ChainofResponsibility.Models;

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
Enumerable.Range(1,10).ToList().ForEach(x =>
{
    dbContext.Products.Add(new Product(){Name = $"kalem1 {x}", Price = 100, Stock = 10});
});
dbContext.SaveChanges();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();