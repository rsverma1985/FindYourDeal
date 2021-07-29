using Companies.Repo;
using Companies.Service;
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

namespace FindYourDeal
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
            var contact = new OpenApiContact()
            {
                Name = "FirstName LastName",
                Email = "user@example.com",
                Url = new Uri("http://www.example.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "My License",
                Url = new Uri("http://www.example.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Swagger Demo API",
                Description = "Swagger Demo API Description",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
            });
        
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
           
            services.AddTransient<IServiceRequest, ServiceRequest>();
           
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

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
