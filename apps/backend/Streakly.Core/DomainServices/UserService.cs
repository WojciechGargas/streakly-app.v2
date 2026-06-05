using Streakly.Core.DomainServices.Interfaces;
using Streakly.Core.Entities;
using Streakly.Core.Exceptions;
using Streakly.Core.Policies.Interfaces;
using Streakly.Core.Repositories;

namespace Streakly.Core.DomainServices;

public class UserService(IUserUpdatePolicy userUpdatePolicy) : IUserService
{
    public Task MarkAsLoggedIn(User requestedBy, User userToUpdate, DateTime loggenAtUtc)
    {
        EnsureCanUpdate(requestedBy, userToUpdate);
        userToUpdate.MarkAsLoggedIn(loggenAtUtc);
        
        return Task.CompletedTask;
    }

    public Task MarkEmailAsConfirmed(User requestedBy, User userToUpdate)
    {
        EnsureCanUpdate(requestedBy, userToUpdate);
        userToUpdate.MarkEmailAsConfirmed();
        
        return Task.CompletedTask;
    }

    public Task ChangeEmail(User requestedBy, User userToUpdate, string newEmail)
    {
        EnsureCanUpdate(requestedBy, userToUpdate);
        userToUpdate.ChangeEmail(newEmail);
        userToUpdate.MarkEmailAsConfirmed();
        
        return Task.CompletedTask;
    }

    private void EnsureCanUpdate(User requestedBy, User userToUpdate)
    {
        if (!userUpdatePolicy.CanUpdate(requestedBy, userToUpdate))
            throw new UserAccessDeniedException();
    }
}