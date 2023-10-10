namespace WebAppTemplate.Abstract;

public class DefaultCardTemplate: UserCardTemplate
{
    protected override string SetFooter()
    {
        return string.Empty;
    }

    protected override string SetPicture()
    {
        return $"<img src='/userPic/defaultPic.png' class='card-img-top'>";
    }
}