using Garage.Models;

namespace Garage.Services
{
    public interface ISeedingService
    {
        Task AddVehicleTypeSeedsAsync(List<VehicleType> vehicleTypeSeeds);
        Task AddVehicleSeedsAsync(List<Vehicle> vehicleSeeds);
        Task AddParkingEventSeedsAsync(List<ParkingEvent> parkingEventSeeds);
        Task<bool> IsDatabaseSeededAsync();
        Task SeedDatabaseAsync();
    }
}
