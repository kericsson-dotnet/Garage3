using Garage.Data;
using Garage.Models;
using Garage.ViewModels;

namespace Garage.Services;

public class GarageService : IGarageService
{
    private readonly IRepository<ParkingEvent> _parkingEventRepository;

    public GarageService(IRepository<ParkingEvent> parkingEventRepository)
    {
        _parkingEventRepository = parkingEventRepository;
    }

    public async Task<List<Vehicle>> GetVehiclesInGarageAsync()
    {
        var parkingEvents = await _parkingEventRepository.GetAll();
        var vehiclesInGarage = parkingEvents.Where(pe => pe.CheckOutTime == null).Select(pe => pe.Vehicle);
        return vehiclesInGarage.ToList();
    }

    public async Task<GarageViewModel> GetGarageStatusAsync()
    {
        var vehiclesInGarage = GetVehiclesInGarageAsync();
        var garageViewModel = new GarageViewModel
        {
            VehiclesInGarage = await vehiclesInGarage,
        };
        return garageViewModel;
    }
}
