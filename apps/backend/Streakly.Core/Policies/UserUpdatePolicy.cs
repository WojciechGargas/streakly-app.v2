using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.Policies.Interfaces;

namespace Streakly.Core.Policies;

public class UserUpdatePolicy : IUserUpdatePolicy
{
    public bool CanUpdate(User requestedBy, User userToUpdate)
        => requestedBy == userToUpdate || requestedBy.Role == UserRole.Admin;
}