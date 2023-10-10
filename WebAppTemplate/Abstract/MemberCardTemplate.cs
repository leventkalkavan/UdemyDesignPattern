using System.Text;

namespace WebAppTemplate.Abstract;

public class MemberCardTemplate: UserCardTemplate
{
    protected override string SetFooter()
    {
        var stringB = new StringBuilder();
        stringB.Append("<a href='#' class='btn btn-primary'>Mesaj Gönder</a>");
        stringB.Append("<a href='#' class='btn btn-primary'>Detaylı Profil</a>");

        return stringB.ToString();
    }

    protected override string SetPicture()
    {
        return $"<img src='{AppUser.PictureUrl}' class='card-img-top'>";
    }
}