using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ToggleSystem.Infra.Identity.Extensions;
using ToggleSystem.Infra.IoC;

namespace ToggleSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddAuthorization()
                    .AddJsonFormatters()
                    .AddApiExplorer()
                    .AddCors();

            services
                .RegisterDependencies(Configuration)
                .AddSecurityConfiguration(Configuration)
                .AddAutoMapper()
                .AddSwaggerGen(s =>
                {
                    s.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "Toggle System",
                        Description = "Swagger Endpoints"
                    });
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
               .UseSwagger()
               .UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Toggle System API v1"))
               .UseAuthentication()
               .UseMvc();
        }
    }
}
