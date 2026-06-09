using Streakly.Core.Enums;
using Streakly.Core.ValueObjects;
using Streakly.Core.ValueObjects.User;

namespace Streakly.Core.Entities;

public class User(
    UserId userId,
    Email email,
    Username username,
    Password password,
    Fullname fullname,
    UserRole role,
    DateTime createdAt,
    DateTime? lastLoggedAtUtc,
    bool isEmailConfirmed = false)
{
    public UserId UserId { get; private set; } = userId;
    public Email Email { get; private set; } = email;
    public Username Username { get; private set; } = username;
    public Password Password { get; private set; } = password;
    public Fullname Fullname { get; private set; } = fullname;
    public UserRole Role { get; private set; } = role;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime? LastLoggedAtUtc { get; private set; } = lastLoggedAtUtc;
    public bool IsEmailConfirmed { get; private set; } = isEmailConfirmed;
    
    public void MarkAsLoggedIn(DateTime loggedAtUtc)
        => LastLoggedAtUtc = loggedAtUtc;
    
    public void MarkEmailAsConfirmed()
        => IsEmailConfirmed = true;

    public void ChangeEmail(string newEmail)
    {
        Email = new Email(newEmail);
        IsEmailConfirmed = false;
    }
}