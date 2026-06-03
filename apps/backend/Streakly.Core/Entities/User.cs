using Streakly.Core.Enums;
using Streakly.Core.ValueObjects;

namespace Streakly.Core.Entities;

public class User(
    UserId userId,
    Email email,
    Username username,
    Password password,
    UserRole role,
    DateTime createdAt,
    DateTime? lastLoggedAtUtc)
{
    public UserId UserId { get; private set; } = userId;
    public Email Email { get; private set; } = email;
    public Username Username { get; private set; } = username;
    public Password Password { get; private set; } = password;
    public UserRole Role { get; private set; } = role;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime? LastLoggedAtUtc { get; private set; } = lastLoggedAtUtc;
}