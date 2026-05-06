using BDA.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BDA.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (TryHandleValidationErrors(errors, out var stateDictionary))
        {
            return ValidationProblem(stateDictionary);
        }

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

    private static bool TryHandleValidationErrors(List<Error> errors, out ModelStateDictionary stateDictionary)
    {
        stateDictionary = new ModelStateDictionary();

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            foreach (var err in errors)
            {
                stateDictionary.AddModelError(err.Code, err.Description);
            }

            return true;
        }

        return false;
    }
}
