using MicroService.Domain;
using MicroService.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult FromResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return result.Value == null
                    ? base.Ok()
                    : base.Ok(result);
            }

            if (Equals(result.Error.Code, Errors.NotFound().Code))
                return base.NotFound(result.Error);

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (Equals(result.Error.Code, Errors.InternalServerError(null).Code))
                return base.StatusCode(500, result.Error);

            return base.BadRequest(result.Error);
        }
    }
}