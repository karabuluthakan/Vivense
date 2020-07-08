using System.Collections.Generic;
using Library.Models.Lookups;

namespace Library.Models.Helpers
{
    public class OrderStage
    {
        public string Code { get; set; }

        public MultiLanguage Title { get; set; } = new MultiLanguage();

        public IEnumerable<OrderStageDetail> Details { get; set; } = new List<OrderStageDetail>();
    }
    
    public class OrderStageDetail
    {
        public string Key { get; set; }

        public MultiLanguage Value { get; set; } = new MultiLanguage();
    }
}