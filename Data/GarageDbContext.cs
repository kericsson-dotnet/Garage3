﻿using Microsoft.EntityFrameworkCore;
using Garage.Models;

namespace Garage.Data;

public class GarageDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<ParkingEvent> ParkingEvents { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }


    protected readonly IConfiguration Configuration;

    public GarageDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingEvent>()
            .HasOne(p => p.Vehicle)
            .WithOne(v => v.ParkingEvent)
            .HasForeignKey<ParkingEvent>(pe => pe.VehicleId);

        modelBuilder.Entity<Vehicle>()
        .HasOne(v => v.User)
        .WithMany(u => u.Vehicles)
        .HasForeignKey(v => v.UserId)
        .OnDelete(DeleteBehavior.Restrict);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sqlite database
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }
}
