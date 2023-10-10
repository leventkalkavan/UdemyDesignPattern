using Microsoft.AspNetCore.Razor.TagHelpers;
using WebAppTemplate.Models;

namespace WebAppTemplate.Abstract;

public class UserCardTagHelper: TagHelper
{
    public AppUser AppUser { get; set; }
    private readonly IHttpContextAccessor _contextAccessor;

    public UserCardTagHelper(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        UserCardTemplate userCardTemplate;
        if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            userCardTemplate = new MemberCardTemplate();
        }
        else
        {
            userCardTemplate = new DefaultCardTemplate();
        }
        userCardTemplate.SetUser(AppUser);
        output.Content.SetHtmlContent(userCardTemplate.Build());
    }
}