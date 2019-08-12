using Microsoft.Extensions.DependencyInjection;
using TaxaJuros.Domain.Services;
using Xunit;

namespace TaxaJuros.Tests
{
    public class TaxaJurosUnitTest
    {
        private readonly ServiceProvider _serviceProvider;

        public TaxaJurosUnitTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ITaxaJurosService, TaxaJurosService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void RetornaTaxaJuros1Porcento()
        {
            var taxaJurosService = _serviceProvider.GetService<ITaxaJurosService>();

            var taxaJuros = taxaJurosService.RetornarTaxaJuros();

            Assert.Equal(0.01M, taxaJuros);
        }
    }
}
