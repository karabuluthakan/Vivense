using Microsoft.AspNetCore.Builder;

namespace Library.IoC.Abstract
{
    public interface IConfigureModule
    {
        int Priority { get; set; }
        void Load(IApplicationBuilder app);
    }
}