using ErrorOr;
using FluentValidation;
using MediatR;

namespace BDA.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            return await next(cancellationToken);
        }

        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (validation.IsValid)
        {
            return await next(cancellationToken);
        }
        
        var errors = validation.Errors.ConvertAll(error => Error.Validation(error.PropertyName, error.ErrorMessage));

        return (dynamic) errors;
    }
}