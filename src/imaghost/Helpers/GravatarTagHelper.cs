using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace imaghost.Helpers
{
    public class GravatarTagHelper : TagHelper
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public int? Size { get; set; }

        public string Class { get; set; }

        public string Alt { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            if (!string.IsNullOrWhiteSpace(Id))
            {
                output.Attributes.SetAttribute("id", Id);
            }
            output.Attributes.SetAttribute("alt", Alt);
            output.Attributes.SetAttribute("src", GenerateGravatarUrl(Email));
            if (!string.IsNullOrWhiteSpace(Class))
            {
                output.Attributes.SetAttribute("class", Class);
            }
            if (Size.HasValue)
            {
                output.Attributes.SetAttribute("width", Size.ToString());
                output.Attributes.SetAttribute("height", Size.ToString());
            }
        }

        private static string GenerateGravatarUrl(string email)
        {
            var md5 = MD5.Create();
            var encoder = new UTF8Encoding();
            var md5Hash = md5.ComputeHash(encoder.GetBytes(email.Trim().ToLower()));
            var md5String = new StringBuilder(md5Hash.Length * 2);
            foreach (var character in md5Hash)
            {
                md5String.Append(character.ToString("X2"));
            }
            return $"https://www.gravatar.com/avatar/{md5String.ToString().ToLower()}?d=mm";
        }
    }
}