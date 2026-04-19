using BDA.Api.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BDA.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        
        var error = errors.First();
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _  => StatusCodes.Status500InternalServerError
        };
        
        return Problem(statusCode: statusCode, title: error.Description);
    }
}