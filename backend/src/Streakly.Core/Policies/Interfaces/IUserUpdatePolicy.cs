using Streakly.Core.Entities;

namespace Streakly.Core.Policies.Interfaces;

public interface IUserUpdatePolicy
{
    bool CanUpdate(User requestedBy, User userToUpdate);
}