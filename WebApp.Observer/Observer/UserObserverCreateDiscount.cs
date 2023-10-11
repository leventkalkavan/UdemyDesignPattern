using WebApp.Observer.Context;
using WebApp.Observer.Models;

namespace WebApp.Observer.Observer;

public class UserObserverCreateDiscount: IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverCreateDiscount(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();
        var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Discounts.Add(new Discount {Rate = 10, UserId = appUser.Id});
        context.SaveChanges();
        logger.LogInformation("Discount created.");
    }
}