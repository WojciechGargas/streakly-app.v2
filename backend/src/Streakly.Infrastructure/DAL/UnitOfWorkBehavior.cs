using MediatR;

namespace Streakly.Infrastructure.DAL;

internal sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
        => unitOfWork.ExecuteAsync(() => next(cancellationToken));
}