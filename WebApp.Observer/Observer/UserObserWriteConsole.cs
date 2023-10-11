using WebApp.Observer.Models;

namespace WebApp.Observer.Observer;

public class UserObserWriteConsole: IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserWriteConsole(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserWriteConsole>>();
        logger.LogInformation($"created by the name of {appUser.UserName}, has id {appUser.Id}.");
    }
}