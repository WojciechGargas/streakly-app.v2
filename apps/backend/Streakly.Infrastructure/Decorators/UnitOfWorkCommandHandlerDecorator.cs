using Streakly.Application.Abstractions;
using Streakly.Infrastructure.DAL;

namespace Streakly.Infrastructure.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler,
    IUnitOfWork unitOfWork) : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    public async Task HandleAsync(TCommand command)
    {
        await unitOfWork.ExecuteAsync(() => commandHandler.HandleAsync(command));
    }
}
