using Microsoft.EntityFrameworkCore;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Handlers;

public class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly MySpotDbContext _dbContext;

    public GetUserHandler(MySpotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Where(x => x.Id == new UserId(query.UserId))
            .Select(x => x.AsDto())
            .SingleOrDefaultAsync();

        return user ?? throw new ApplicationException("User not found.");
    }
}