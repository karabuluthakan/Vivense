using Library.Aspects;
using Library.CrossCuttingConcerns.Authorization;
using Library.CrossCuttingConcerns.Authorization.Abstract;
using Library.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Identity;

namespace Api.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEntityHierarchyProvider, EntityHierarchyMemoryProvider>();
            ServiceTool.Create(services);
            // services.AddTransient<SiteScopeAttribute>(); 
            services.AddControllers(options =>
            {
                options.Filters.Add<SiteScopeFilter>();
            });
            services.AddScoped<IWeatherForecastService, WeatherForecastManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}