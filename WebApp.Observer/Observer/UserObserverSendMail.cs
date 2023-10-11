using System.Net;
using System.Net.Mail;
using WebApp.Observer.Models;

namespace WebApp.Observer.Observer;

public class UserObserverSendMail: IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverSendMail(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendMail>>();
        // var mailMessage = new MailMessage();
        // var smptClient = new SmtpClient("siteclient.com");
        // mailMessage.From = new MailAddress("sitedekimailadresi.com");
        // mailMessage.To.Add(new MailAddress(appUser.Email));
        // mailMessage.Subject = "siteye subject hos geldiniz cart curt";
        // mailMessage.Body = "<p>siteye body hos geldiniz cart curt</p>";
        // mailMessage.IsBodyHtml = true;
        //siteportu tamamen sallamaca
        // smptClient.Port = 1907;
        // smptClient.Credentials = new NetworkCredential("sitedekimailadresi.com","mailsifresi");
        // smptClient.Send(mailMessage);
        logger.LogInformation($"Email sent to a named {appUser.UserName}.");

    }
}