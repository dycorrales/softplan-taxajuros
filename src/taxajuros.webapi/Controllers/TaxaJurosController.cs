using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TaxaJuros.Domain.Services;

namespace TaxaJuros.WebApi.Controllers
{
    [Route("taxajuros")]
    public class TaxaJurosController : BaseController
    {
        private readonly ITaxaJurosService _taxaJurosService;

        public TaxaJurosController(ITaxaJurosService taxaJurosService) : base()
        {
            _taxaJurosService = taxaJurosService;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retorna Taxa Juros", Tags = new[] { "Taxa Juros" })]
        public IActionResult GetTaxaJuros()
        {
            try
            {
                var taxaJuros = _taxaJurosService.RetornarTaxaJuros();

                return RequestResponse(HttpStatusCode.OK, result: JsonConvert.SerializeObject(taxaJuros));
            }
            catch
            {
                return RequestResponse(HttpStatusCode.BadRequest, isError: true, result: "Ocorreu um erro ao retornar a taxa de juros");
            }
        }

        [HttpGet("showmethecode")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [SwaggerOperation(Summary = "Link para Código Fonte", Tags = new[] { "Taxa Juros" })]
        public IActionResult ShowmeTheCode()
        {
            return RequestResponse(HttpStatusCode.OK, result: "https://github.com/dycorrales/softplan-taxajuros");
        }

        [HttpGet("healthcheck")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [SwaggerOperation(Summary = "Retorna OK", Tags = new[] { "Taxa Juros" })]
        public IActionResult GetOk()
        {
            return RequestResponse(HttpStatusCode.OK);
        }
    }
}
