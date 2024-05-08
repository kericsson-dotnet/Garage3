using Garage.Data;
using Garage.Models;
using Garage.Services;
using System.Text.Json;

public class SeedingService : ISeedingService
{
    private readonly IRepository<Vehicle> _vehicleRepository;
    private readonly IRepository<VehicleType> _vehicleTypeRepository;
    private readonly IRepository<ParkingEvent> _parkingEventRepository;

    public SeedingService(IRepository<Vehicle> vehicleRepository, IRepository<VehicleType> vehicleTypeRepository, IRepository<ParkingEvent> parkingEventRepository)
    {
        _vehicleRepository = vehicleRepository;
        _vehicleTypeRepository = vehicleTypeRepository;
        _parkingEventRepository = parkingEventRepository;
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

    public async Task AddParkingEventSeedsAsync(List<ParkingEvent> parkingEventSeeds)
    {
        foreach (var parkingEvent in parkingEventSeeds)
        {
            await _parkingEventRepository.Add(parkingEvent);
        }
    }

    private List<T> LoadSeedsToList<T>(string relativePath)
    {
        var fullPath = Path.GetFullPath(relativePath);
        var json = System.IO.File.ReadAllText(fullPath);
        var seeds = JsonSerializer.Deserialize<List<T>>(json);
        return seeds!;
    }

    private List<Vehicle> LoadVehicleSeedsToList()
    {
        var relativePath = Path.Combine("Data", "MockupData", "SeedVehicles.txt");
        return LoadSeedsToList<Vehicle>(relativePath);
    }

    private List<VehicleType> LoadVehicleTypeSeedsToList()
    {
        var relativePath = Path.Combine("Data", "MockupData", "SeedVehicleTypes.txt");
        return LoadSeedsToList<VehicleType>(relativePath);
    }

    private List<ParkingEvent> LoadParkingEventSeedsToList()
    {
        var relativePath = Path.Combine("Data", "MockupData", "SeedParkingEvents.txt");
        return LoadSeedsToList<ParkingEvent>(relativePath);
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

        var parkingEventSeeds = LoadParkingEventSeedsToList();
        await AddParkingEventSeedsAsync(parkingEventSeeds);
    }

}