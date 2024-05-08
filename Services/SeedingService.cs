using Garage.Data;
using Garage.Models;
using Garage.Services;
using System.Text.Json;

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

    private List<Vehicle> LoadVehicleSeedsToList()
    {
        var relativePath = Path.Combine("Data", "MockupData", "SeedVehicles.txt");
        var fullPath = Path.GetFullPath(relativePath);
        var json = System.IO.File.ReadAllText(fullPath);
        var vehicleSeeds = JsonSerializer.Deserialize<List<Vehicle>>(json);
        return vehicleSeeds!;
    }

    // TODO: break out the duplicate code
    private List<VehicleType> LoadVehicleTypeSeedsToList()
    {
        var relativePath = Path.Combine("Data", "MockupData", "SeedVehicleTypes.txt");
        var fullPath = Path.GetFullPath(relativePath);
        var json = System.IO.File.ReadAllText(fullPath);
        var vehicleTypeSeeds = JsonSerializer.Deserialize<List<VehicleType>>(json);
        return vehicleTypeSeeds!;
    }
    // Handles the check to see if Db is already seeded with stuff. 
    public async Task<bool> IsDatabaseSeededAsync()
    {
        var vehicles = await _vehicleRepository.GetAll();
        return vehicles.Any(v => v.RegNumber == "ZZT871");
    }

    // Make sure to run them in correct order (when we add users etc)
    public async Task SeedDatabaseAsync()
    {   
        var vehicleTypeSeeds = LoadVehicleTypeSeedsToList();
        await AddVehicleTypeSeedsAsync(vehicleTypeSeeds);

        var vehicleSeeds = LoadVehicleSeedsToList();
        await AddVehicleSeedsAsync(vehicleSeeds);
    }

}