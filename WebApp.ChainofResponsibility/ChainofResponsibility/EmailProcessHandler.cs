using System.Net.Mail;
using System.Net.Mime;

namespace WebApp.ChainofResponsibility.ChainofResponsibility;

public class EmailProcessHandler: ProcessHandler
{
    private readonly string _fileName;
    private readonly string _toEmail;

    public EmailProcessHandler(string fileName, string toEmail)
    {
        _fileName = fileName;
        _toEmail = toEmail;
    }

    public override object handle(object o)
    {
        //var zipMemory = o as MemoryStream;
        //zipMemory.Position = 0;
        // var mailMessage = new MailMessage();
        // var smptClient = new SmtpClient("siteclient.com");
        // mailMessage.From = new MailAddress("sitedekimailadresi.com");
        // mailMessage.To.Add(new MailAddress(appUser.Email));
        // mailMessage.Subject = "Zip Dosyasi";
        // mailMessage.Body = "<p>Zip dosyasi ekte</p>";
        //Attachment attachment = new Attachment(zipMemory, _fileName, MediaTypeNames.Application.Zip);
        // mailMessage.IsBodyHtml = true;   
        //siteportu tamamen sallamaca
        // smptClient.Port = 1907;
        // smptClient.Credentials = new NetworkCredential("sitedekimailadresi.com","mailsifresi");
        // smptClient.Send(mailMessage);
        return base.handle(o);
    }
}