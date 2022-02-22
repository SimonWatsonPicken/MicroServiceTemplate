using MediatR;
using MicroService.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MicroService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : BaseController
    {
        private readonly IMediator _mediator;

        public Controller(IMediator mediator)
        {
            Log.Debug("In Controller::ctor...");
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator), "Mediator cannot be null.");
        }

        [HttpGet("book")]
        public async Task<IActionResult> GetBookInformation(string isbn)
        {
            // 1852244003.
            var query = new GetBookByIsbnQuery(isbn);
            var result = await _mediator.Send(query);

            return FromResult(result);
        }
    }
}