namespace Customers.Api.Controllers
{
    using Customers.Api.Dto;
    using Customers.Application.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    [ApiController]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator mediator;

        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<IActionResult> ExecuteCommandAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            try
            {
                TResponse response = await this.mediator.Send(request, cancellationToken);
                return new JsonResult(response) { StatusCode = (int)statusCode };
            }

            // Failures in validation copy to error response
            catch (FluentValidation.ValidationException ex)
            {
                ErrorResponseDto errorResponse = new ErrorResponseDto();

                List<ErrorDto> errors = new List<ErrorDto>();

                foreach (FluentValidation.Results.ValidationFailure error in ex.Errors)
                {
                    errors.Add(new ErrorDto(error.ErrorCode, error.ErrorMessage));
                }
                errorResponse.Errors = errors.ToArray();

                return new JsonResult(errorResponse) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (NotFoundException)
            {
                return this.NotFound();
            }
            catch (Exception ex)
            {
                return this.InteralError(ex);
            }
        }

        protected async Task<IActionResult> ExecuteCommandAsync<TResponse>(IRequest<TResponse> request, Func<TResponse, object> success, CancellationToken cancellationToken = default, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            try
            {
                TResponse response = await this.mediator.Send(request, cancellationToken);
                return new JsonResult(success(response)) { StatusCode = (int)statusCode };
            }

            // Failures in validation copy to error response
            //catch (FluentValidation.ValidationException ex)
            //{
            //    ErrorResponse errorResponse = new ErrorResponse();

            //    List<Error> errors = new List<Error>();

            //    foreach (FluentValidation.Results.ValidationFailure error in ex.Errors)
            //    {
            //        errors.Add(new Error(error.ErrorCode, error.ErrorMessage));
            //    }
            //    errorResponse.Errors = errors.ToArray();

            //    return new JsonResult(errorResponse) { StatusCode = (int)HttpStatusCode.BadRequest };
            //}
            catch (NotFoundException)
            {
                return this.NotFound();
            }
            catch (Exception ex)
            {
                return this.InteralError(ex);
            }
        }

        private JsonResult InteralError(Exception ex)
        {
            ErrorResponseDto errorResponse = new ErrorResponseDto();
            List<ErrorDto> errors = new List<ErrorDto>
                {
                    new ErrorDto(ex.GetType().Name, ex.Message)
                };
            errorResponse.Errors = errors.ToArray();
            return new JsonResult(errorResponse) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
    }
}