using System.Net.Http;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Net;
using TaxaJuros.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TaxaJuros.Tests
{
    public class TaxaJurosControllerIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public TaxaJurosControllerIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetTaxaJuros1PorcentoAsync()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/retornataxajuros/api/v1/taxajuros");

            response.EnsureSuccessStatusCode();
            var taxaJuros = 0M;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<Result>().Result;
                taxaJuros = Convert.ToDecimal(result.Data, CultureInfo.InvariantCulture);
            }

            Assert.Equal(0.01M, taxaJuros);
        }

        [Fact]
        public async Task GetOkAsync()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/retornataxajuros/api/v1/taxajuros/healthcheck");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

    public class Result
    {
        public string Response { get; set; }
        public string Data { get; set; }
    }
}
