using TaxaJuros.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO.Compression;
using TaxaJuros.Domain.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TaxaJuros.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();

            services.Configure<GzipCompressionProviderOptions>(
                opt => opt.Level = CompressionLevel.Optimal
                );

            services.AddResponseCompression(opt =>
            {
                opt.Providers.Add<GzipCompressionProvider>();
                opt.EnableForHttps = true;
            });

            services.AddMvc(opt =>
            {
                opt.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt => opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            services.AddCors();

            services.AddApiVersioning("retornataxajuros/api/v{version}");

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });
            services.AddSwaggerConfig();
            
            services.AddSingleton(provider => Configuration);
            services.AddScoped<ITaxaJurosService, TaxaJurosService>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyHeader();
                c.AllowAnyMethod();
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Retorna Taxa Juros API v1.0");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}
