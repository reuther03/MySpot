using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.DAL.Converters;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.Property(x => x.EmployeeName)
            .IsRequired()
            .HasConversion(x => x.Value, x => new EmployeeName(x));
        builder.Property(x => x.LicencePlate)
            .IsRequired()
            .HasConversion(x => x.Value, x => new LicencePlate(x));

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasConversion<UserIdConverter>();
    }
}