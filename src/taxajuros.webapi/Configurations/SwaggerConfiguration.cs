using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
namespace TaxaJuros.WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Taxa Juros",
                    Description = "API Para Retornar Taxa de Juros"
                });
            });
        }
    }
}
