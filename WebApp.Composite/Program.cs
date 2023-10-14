using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Composite.Context;
using WebApp.Composite.Models;

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

    var newUser = new AppUser{ UserName = "levent", Email = "levent@gmail.com" };
    userManager.CreateAsync(newUser, "P123s.").Wait();
    userManager.CreateAsync(new AppUser{ UserName = "bir", Email = "birone@gmail.com" },"P123.s").Wait();
    
    var newCategory1 = new Category { Name = "suc romanlari", ReferenceId = 0, UserId = newUser.Id };
    var newCategory2 = new Category { Name = "cinayet romanlari", ReferenceId = 0, UserId = newUser.Id };
    var newCategory3 = new Category { Name = "polisiye romanlari", ReferenceId = 0, UserId = newUser.Id };

    dbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);

    dbContext.SaveChanges();

    var subCategory1 = new Category { Name = "suc romanlari 1", ReferenceId = newCategory1.Id, UserId = newUser.Id };
    var subCategory2 = new Category { Name = "cinayet romanlari 1", ReferenceId = newCategory2.Id, UserId = newUser.Id };
    var subCategory3 = new Category { Name = "polisiye romanlari 1", ReferenceId = newCategory3.Id, UserId = newUser.Id };

    dbContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);
    dbContext.SaveChanges();

    var subCategory4 = new Category { Name = "cinayet romanlari 1.1", ReferenceId = subCategory2.Id, UserId = newUser.Id };

    dbContext.Categories.Add(subCategory4);
    dbContext.SaveChanges();
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