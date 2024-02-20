using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repository;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(MySpotDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<User> GetByIdAsync(UserId id)
        => await _users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetByUsernameAsync(string username)
        => await _users.SingleOrDefaultAsync(x => x.Username == username);

    public async Task<User> GetByEmailAsync(string email)
        => await _users.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
    }
}