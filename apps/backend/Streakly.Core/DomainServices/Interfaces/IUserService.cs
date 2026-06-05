using Streakly.Core.Entities;

namespace Streakly.Core.DomainServices.Interfaces;

public interface IUserService
{
    Task MarkAsLoggedIn(User requestedBy, User userToUpdate, DateTime loggenAtUtc);
}