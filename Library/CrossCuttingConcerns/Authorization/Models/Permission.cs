using System.Collections.Generic;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class Permission
    {
        public IEnumerable<AccessRight> OpCodes { get; set; } = new List<AccessRight>();
        public IEnumerable<AccessRight> StageCodes { get; set; } = new List<AccessRight>();
    }
}