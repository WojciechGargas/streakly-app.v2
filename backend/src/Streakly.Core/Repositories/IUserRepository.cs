using Streakly.Core.Entities;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(UserId userId);
    Task<User?> GetUserByEmailAsync(Email email);
    Task<User?> GetUserByUserNameAsync(string userName);
    Task AddUserAsync(User user);
}