using Library.IoC;
using Library.IoC.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Service.Identity.DataAccess;
using Service.Identity.DataAccess.Abstract;

namespace Service.Identity.DependencyResolvers.ConfigureServices
{
    public class AddCrudDependencies : IConfigureServiceModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddScoped<IEntityRepository,EntityRepository>();
        }
    }
}