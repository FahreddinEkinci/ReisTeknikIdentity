using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ReisTeknikIdentity.Web.TagHelpers
{
    public class ImageThumbnail:TagHelper
    {
        public string ImageSrc { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
           
            output.TagName= "img";

            string FileName = ImageSrc.Split(".")[0];
            string FileExtension = Path.GetExtension(ImageSrc);

            output.Attributes.SetAttribute("src", $"{FileName}-100x100 {FileExtension}");    

        }
    }
}
