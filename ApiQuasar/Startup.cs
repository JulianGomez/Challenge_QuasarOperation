using ApiQuasar.Adapters;
using ApiQuasar.Adapters.Interfaces;
using ApiQuasar.Filters;
using ApiQuasar.Services;
using ApiQuasar.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiQuasar
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiQuasar", Version = "v1" });
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(item: new HttpExceptionsFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IValidatorService, ValidatorService>();
            services.AddSingleton<IAdapterRecoveryMessage, AdapterRecoveryMessage>();
            services.AddSingleton<IAdapterPositionSatellite, AdapterPositionSatellite>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiQuasar v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}