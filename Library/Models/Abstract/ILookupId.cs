using System;

namespace Library.Models.Abstract
{
    public interface ILookupId<out TKey> : ILookup where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }
}