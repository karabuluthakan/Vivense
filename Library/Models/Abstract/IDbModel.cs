using System;
using System.Collections.Generic;
using Library.Models.DbHelpers;
using Library.Models.Lookups;

namespace Library.Models.Abstract
{
    public interface IDbModel<out TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        DateTime CreateDate { get; }
        bool Genesis { get; }
        bool Deleted { get; }
        OwnerDetail Owner { get; }
        IEnumerable<SharedWithDetail> Shared { get; } 
    }
}