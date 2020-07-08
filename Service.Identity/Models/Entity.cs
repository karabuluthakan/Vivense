using System.Collections.Generic;
using Library.Models.Abstract;
using Library.Models.Enums;
using Library.Models.Helpers;
using Library.Models.Lookups;
using Library.Utilities.Uploads;
using Service.Identity.Models.Enums;

namespace Service.Identity.Models
{
    public class Entity : DbModel
    {
        public string ParentEntityId { get; set; }
        public EntityType Type { get; set; }
        public EntitySubType SubType { get; set; }
        public string Name { get; set; }
        public UploadedFileInfo Avatar { get; set; } = new UploadedFileInfo();
        public string Website { get; set; }
        public Segment Segment { get; set; } = Segment.None;
        public List<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();
        public List<Address> Addresses { get; set; } = new List<Address>();
        public AccountingInfo AccountingInfo { get; set; } = new AccountingInfo();
        public TaxInfo TaxInfo { get; set; } = new TaxInfo();
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public LookupIdName AccountManager { get; set; } = new LookupIdName();
        public List<UploadedFileInfo> TaxDocuments { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> ContractDocuments { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> GeneralDocuments { get; set; } = new List<UploadedFileInfo>();
        public string Code { get; set; }
        public bool IsActive { get; set; } = true;
        public List<LookupIdName> GrantedSites { get; set; }
    }
}