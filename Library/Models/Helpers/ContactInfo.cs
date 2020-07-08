using Library.Models.Lookups;

namespace Library.Models.Helpers
{
    public class ContactInfo
    {
        public string Name { get; set; }
        public MultiLanguage Title { get; set; } = new MultiLanguage();
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}