using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Converters;

public class UserIdConverter : ValueConverter<UserId, Guid>
{
    public UserIdConverter() : base(
        x => x.Value,
        x => new UserId(x)
    )
    {
    }
}