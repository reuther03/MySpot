﻿using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Repository;

internal sealed class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly MySpotDbContext _dbContext;
    private readonly DbSet<WeeklyParkingSpot> _weeklyParkingSpots;


    public PostgresWeeklyParkingSpotRepository(MySpotDbContext dbContext)
    {
        _dbContext = dbContext;
        _weeklyParkingSpots = _dbContext.WeeklyParkingSpots;
    }

    public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots.ToList();

    public IEnumerable<WeeklyParkingSpot> GetByWeek(Week week) =>
        _weeklyParkingSpots.Where(x => x.Week == week).ToList();

    public WeeklyParkingSpot Get(ParkingSpotId id)
        => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id);

    public void Add(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
        _dbContext.SaveChanges();
    }

    public void Update(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Update(weeklyParkingSpot);
        _dbContext.SaveChanges();
    }
}