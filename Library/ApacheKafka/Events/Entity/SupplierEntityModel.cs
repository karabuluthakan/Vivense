using System.Collections.Generic;
using Library.Models.Helpers;
using Library.Models.Lookups;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.Entity
{
    public class SupplierEntityModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string EntityId { get; set; }
        public List<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();

        public UploadedFileInfo Avatar { get; set; } = new UploadedFileInfo();
        public SegmentType? Segment { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();
        public string Website { get; set; }
        public AccountingInfo AccountingInfo { get; set; } = new AccountingInfo();
        public TaxInfo TaxInfo { get; set; } = new TaxInfo();
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public LookupIdName AccountManager { get; set; } = new LookupIdName();
        public List<UploadedFileInfo> TaxDocuments { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> ContractDocuments { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> GeneralDocuments { get; set; } = new List<UploadedFileInfo>();

        public string Code { get; set; }
    }
}
