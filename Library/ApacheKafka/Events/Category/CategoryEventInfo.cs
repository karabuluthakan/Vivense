using System;
using System.Collections.Generic;
using Library.Models.Lookups;
using Library.Utilities.Uploads;

namespace Library.ApacheKafka.Events.Category
{
    public class CategoryEventInfo
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public int LegacyId { get; set; }
        public string ParentId { get; set; }
        public string ParentLegacyId { get; set; }
        public bool Published { get; set; }
        public MultiLanguage Title { get; set; } = new MultiLanguage();
        public MultiLanguage Summary { get; set; } = new MultiLanguage();
        public List<string> Tags { get; set; } = new List<string>();
        public LookupIdName AccountManager { get; set; } = new LookupIdName();
        public double DefaultCommissionRate { get; set; }
        public List<UploadedFileInfo> Media { get; set; } = new List<UploadedFileInfo>();
    }
}
