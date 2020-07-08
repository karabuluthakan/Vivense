using Microsoft.Extensions.DependencyInjection;

namespace Library.IoC.Abstract
{
    public interface IConfigureServiceModule
    {
        void Load(IServiceCollection services);
    }
}