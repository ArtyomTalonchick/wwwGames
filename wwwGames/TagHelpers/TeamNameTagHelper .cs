using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace wwwGames.TagHelpers
{
    public class TeamNameTagHelper : TagHelper
    {
        public string id { get; set; }
        ContextDb db;

        public TeamNameTagHelper(ContextDb context)
        {
            db = context;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.Attributes.SetAttribute("class", "large");
            output.Content.SetContent(db.Teams.FirstOrDefault(t => t.Id.ToString() == id).Name);
        }
    }
}
