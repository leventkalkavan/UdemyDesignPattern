using System.Text;
using Microsoft.Extensions.Primitives;
using WebAppTemplate.Models;

namespace WebAppTemplate.Abstract;

public abstract class UserCardTemplate
{
    protected AppUser AppUser;

    public void SetUser(AppUser appUser)
    {
        AppUser = appUser;
    }

    public string Build()
    {
        if (AppUser == null) throw new ArgumentNullException(nameof(AppUser));

        var stringB = new StringBuilder();
        
        stringB.Append("<div class='card'>");
        stringB.Append(SetPicture());
        stringB.Append($@"<div class='card-body'>
            <h5>{AppUser.UserName}</h5>
            <h5>{AppUser.Description}</h6>");
        stringB.Append(SetFooter());
        stringB.Append("</div>");
        stringB.Append("</div>");
        
        return stringB.ToString();
    }
    protected abstract string SetFooter();
    protected abstract string SetPicture();
}