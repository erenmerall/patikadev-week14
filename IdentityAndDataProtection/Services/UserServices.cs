using Microsoft.AspNetCore.Identity;
using IdentityAndDataProtection.Models;
using IdentityAndDataProtection.Data;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(string email, string password)
    {
        var user = new User
        {
            Email = email
        };

        // Password hashleme
        user.Password = _passwordHasher.HashPassword(user, password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }
}
