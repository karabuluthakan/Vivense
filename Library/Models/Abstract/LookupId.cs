namespace Library.Models.Abstract
{
    public abstract class LookupId : ILookupId<string>
    {
        public string Id { get; set; }
    }
}