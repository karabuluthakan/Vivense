using Library.Models.Abstract;

namespace Library.Models.Lookups
{
    public class LookupIdTitle : LookupId
    {
        public MultiLanguage Title { get; set; }
    }
}