using Library.Models.Enums;
using Library.Models.Lookups;

namespace Library.Models.Helpers
{
    public class Address
    {
        public AddressType? Type { get; set; } = AddressType.Billing;
        public string Street { get; set; }
        public LookupIdName District { get; set; }
        public LookupIdName County { get; set; }
        public LookupIdName City { get; set; }
        public string ZipCode { get; set; }
        public LookupCodeTitle Country { get; set; }
        public GeoLocation GeoLocation { get; set; } = new GeoLocation();
    }
}