using System.Collections.Generic;
using Library.Models.Abstract;

namespace Library.Models
{
    public class TaxEventInfo: DbModel
    {
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public List<string> CityCodes { get; set; } = new List<string>();

        public List<string> Categories { get; set; } = new List<string>();

        public double Rate { get; set; }
        public bool Compound { get; set; }
        public int Priority { get; set; }
    }
}