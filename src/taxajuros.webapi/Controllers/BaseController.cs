using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TaxaJuros.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
        }

        protected IActionResult RequestResponse(HttpStatusCode httpStatusCode, string uri = null, bool isError = false, object result = null)
        {
            if (!isError)
            {
                if (httpStatusCode == HttpStatusCode.Created)
                    return Created(uri, new
                    {
                        success = true,
                        data = result
                    });

                if (httpStatusCode == HttpStatusCode.NotFound)
                    return Created(uri, new
                    {
                        success = true,
                        data = result
                    });

                if (httpStatusCode == HttpStatusCode.NoContent)
                    return Created(uri, new
                    {
                        success = true,
                        data = result
                    });

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            switch (httpStatusCode)
            {
                case HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(new
                    {
                        success = false,
                        errors = result
                    });
                case HttpStatusCode.NotFound:
                    return NotFound(new
                    {
                        success = false,
                        errors = result
                    });
                case HttpStatusCode.Conflict:
                    return NotFound(new
                    {
                        success = false,
                        errors = result
                    });
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                case HttpStatusCode.InternalServerError:
                    return new InternalServerError(result);
                default:
                    return BadRequest(new
                    {
                        success = false,
                        errors = result
                    });
            }
        }

        public class InternalServerError : ObjectResult
        {
            public InternalServerError(object value) : base(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }

            public InternalServerError() : this(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
