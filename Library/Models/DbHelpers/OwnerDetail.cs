using Library.Models.Lookups;

namespace Library.Models.DbHelpers
{
    public class OwnerDetail
    {
        public string EntityId { get; set; }
        public string UserId { get; set; }
        public LookupIdName SiteInfo { get; set; }
    }
}