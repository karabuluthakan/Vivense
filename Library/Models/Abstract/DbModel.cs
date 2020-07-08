using System;
using System.Collections.Generic;
using Library.Models.DbHelpers;

namespace Library.Models.Abstract
{
    public abstract class DbModel : IDbModel<string>
    {
        public string Id { get; }
        public DateTime CreateDate { get; protected set; } = DateTime.Now;
        public int LegacyId { get; } = 1;
        public bool Genesis { get; protected set; } = false;
        public bool Deleted { get; protected set; } = false;
        public OwnerDetail Owner { get; } = null;
        public IEnumerable<SharedWithDetail> Shared { get; } = new List<SharedWithDetail>();
    }
}