namespace Customers.Application.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class Command<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}