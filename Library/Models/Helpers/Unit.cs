using System;
using Library.Models.Lookups;

namespace Library.Models.Helpers
{
    public class Unit
    {
        public Guid Id { get; set; }
        public MultiLanguage Value { get; set; } = new MultiLanguage();
    }
}