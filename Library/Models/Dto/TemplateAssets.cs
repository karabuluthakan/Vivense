using System.Collections.Generic;

namespace Library.Models.Dto
{
    public class TemplateAssets
    {
        public List<string> CssUrls { get; set; } = new List<string>();
        public List<string> JsUrls { get; set; } = new List<string>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}