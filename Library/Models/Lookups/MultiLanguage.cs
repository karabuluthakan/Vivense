using System.Collections.Generic;
using System.Linq;
using Library.Models.Abstract;

namespace Library.Models.Lookups
{
    public class MultiLanguage : Dictionary<string, string>, ILookup
    {
        public bool Equals(MultiLanguage other)
        {
            if (other == null)
            {
                return false;
            }

            return Count == other.Count && !this.Except(other).Any();
        }
    }
}