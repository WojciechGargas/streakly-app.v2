using Streakly.Core.Entities;

namespace Streakly.Core.DomainServices.Interfaces;

public interface IUserService
{
    Task MarkAsLoggedIn(User requestedBy, User userToUpdate, DateTime loggedAtUtc);
    Task MarkEmailAsConfirmed(User requestedBy, User userToUpdate);
    Task ChangeEmail(User requestedBy, User userToUpdate, string newEmail);
}