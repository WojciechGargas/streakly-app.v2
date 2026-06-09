using Microsoft.EntityFrameworkCore;
using Streakly.Core.Entities;
using Streakly.Core.Repositories;
using Streakly.Core.ValueObjects;


namespace Streakly.Infrastructure.DAL.Repositories;

public class UserRepository(StreaklyDbContext dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = dbContext.Users;
    
    public Task<User?> GetUserByIdAsync(UserId userId)
        => _users.SingleOrDefaultAsync(u => u.UserId == userId);

    public Task<User?> GetUserByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(u => u.Email == email);

    public Task<User?> GetUserByUserNameAsync(string userName)
        => _users.SingleOrDefaultAsync(u => u.Username == userName);

    public async Task AddUserAsync(User user)
        => await _users.AddAsync(user);
}