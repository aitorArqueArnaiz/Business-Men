using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Vueling.Domain.ApiRest_Client;
using Vueling.Domain.Interfaces;
using Vueling.Domain.Services;
using Vueling.Infrastructure.Repository;

namespace Vueling.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vueling.API", Version = "v1" });
            });

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(new ErrorHandlingFilter());
            });


            // Add application services.
            ApiRestClient.Initialize();
            var repository = new Repository();
            services.Add(new ServiceDescriptor(typeof(ITransactionService), new TransactionService(repository)));
            services.Add(new ServiceDescriptor(typeof(IRateService), new RateService(repository)));

            // Add logging service
            services.AddLogging(configure => configure.AddConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vueling.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
