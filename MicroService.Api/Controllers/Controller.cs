using MicroService.Api.Application;
using MicroService.Api.Application.Books;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller : ControllerBase
    {
        private readonly ICommandHandler<BookCommand, BookUiEntity> _bookCommandHandler;

        public Controller(ICommandHandler<BookCommand, BookUiEntity> bookCommandHandler)
        {
            _bookCommandHandler = bookCommandHandler;
        }

        [HttpPost("bookInfo")]
        public async Task<IActionResult> GetBookInformation([FromBody] BookCommand bookCommand)
        {
            var bookUiEntity = await _bookCommandHandler.Handle(bookCommand);

            return Ok(bookUiEntity);
        }
    }
}