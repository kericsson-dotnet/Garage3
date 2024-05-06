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
}