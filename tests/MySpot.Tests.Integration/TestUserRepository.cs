using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Tests.Integration;

public class TestUserRepository : IUserRepository
{
    private readonly List<User> _users = [];

    public async Task<User> GetByIdAsync(UserId id)
    {
        await Task.CompletedTask;
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        await Task.CompletedTask;
        return _users.FirstOrDefault(u => u.Username == username);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        await Task.CompletedTask;
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        _users.Add(user);
        await Task.CompletedTask;
    }
}