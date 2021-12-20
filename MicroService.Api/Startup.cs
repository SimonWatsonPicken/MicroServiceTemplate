using MicroService.Api.Application;
using MicroService.Api.Application.Books;
using MicroService.Infrastructure.Options;
using MicroService.Infrastructure.ThirdPartyServices.OpenLibraryService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MicroService.Api
{
    [ExcludeFromCodeCoverage]
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
                c.SwaggerDoc("v1.0.0", new OpenApiInfo
                {
                    Title = "MicroService.Api",
                    Version = "v1.0.0"
                });
                c.IncludeXmlComments($"{Assembly.GetEntryAssembly()?.GetName().Name}.xml");
            });

            // Command handlers.
            services.AddScoped<ICommandHandler<BookCommand, BookUiEntity>, BookCommandHandler>();

            // Command validators.
            services.AddScoped<IValidator<BookCommand>, BookCommandValidator>();

            // HTTP clients.
            services.AddHttpClient<IOpenLibraryService, OpenLibraryService>();

            // Options.
            services.Configure<OpenLibraryServiceOptions>(Configuration.GetSection(OpenLibraryServiceOptions.OpenLibraryServiceSectionName));
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<OpenLibraryServiceOptions>, OpenLibraryServiceOptionsValidator>());

            // Services.
            services.AddScoped<IOpenLibraryService, OpenLibraryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1.0.0/swagger.json", "MicroService.Api v1.0.0"));
            }

            // Ensure this line is after any health check middleware.
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}