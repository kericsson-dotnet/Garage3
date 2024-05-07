using Garage.Data;
using Garage.Models;
using Garage.Services;

public class SeedingService : ISeedingService
{
    private readonly IRepository<Vehicle> _vehicleRepository;
    private readonly IRepository<VehicleType> _vehicleTypeRepository;

    public SeedingService(IRepository<Vehicle> vehicleRepository, IRepository<VehicleType> vehicleTypeRepository)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task AddVehicleSeedsAsync(List<Vehicle> vehicleSeeds)
    {
        foreach (var vehicle in vehicleSeeds)
        {
            await _vehicleRepository.Add(vehicle);
        }
    }

    public async Task AddVehicleTypeSeedsAsync(List<VehicleType> vehicleTypeSeeds)
    {
        foreach (var vehicleType in vehicleTypeSeeds)
        {
            await _vehicleTypeRepository.Add(vehicleType);
        }
    }

    // Handles the check to see if Db is already seeded with stuff. Persist across requests.
    public async Task<bool> IsDatabaseSeededAsync()
    {
        var vehicles = await _vehicleRepository.GetAll();
        return vehicles.Any(v => v.RegNumber == "ZZT871");
    }
}