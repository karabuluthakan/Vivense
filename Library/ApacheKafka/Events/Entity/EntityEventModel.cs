using System.Collections.Generic;
using Library.Models.DbHelpers;
using Library.Models.Enums;
using Library.Models.Helpers;
using Library.Models.Lookups;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityEventModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }

        public EntityType Type { get; set; }
        public EntitySubType SubType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OwnerDetail Owner { get; set; } = null;

        public List<SharedWithDetail> SharedWith { get; set; } = new List<SharedWithDetail>();

        /// <summary>
        /// 
        /// </summary>
        public List<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();

        /// <summary>
        /// 
        /// </summary>
        public UploadedFileInfo Avatar { get; set; } = new UploadedFileInfo();

        public List<Address> Addresses { get; set; }

        public SegmentEventModel Segment { get; set; } = SegmentEventModel.None;

        public AccountingInfo AccountingInfo { get; set; } = new AccountingInfo();
        public TaxInfo TaxInfo { get; set; } = new TaxInfo();
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

        public LookupIdName AccountManager { get; set; } = new LookupIdName();

        public List<UploadedFileInfo> TaxDocuments { get; set; } = new List<UploadedFileInfo>();

        public List<UploadedFileInfo> ContractDocuments { get; set; } = new List<UploadedFileInfo>();
        public List<UploadedFileInfo> GeneralDocuments { get; set; } = new List<UploadedFileInfo>();

        /// <summary>
        /// For SKU etc..
        /// </summary>
        public string Code { get; set; }
    }

    public enum SegmentEventModel
    {
        None = 0,
        Starter,
        Bronze,
        Silver,
        Gold,
        Platinum
    }
}