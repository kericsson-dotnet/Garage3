using Garage.Data;
using Garage.Models;
using Garage.ViewModels;

namespace Garage.Services;

public class GarageService : IGarageService
{
    private readonly IRepository<ParkingEvent> _parkingEventRepository;
    private readonly IRepository<Vehicle> _vehicleRepository;

    public GarageService(IRepository<ParkingEvent> parkingEventRepository, IRepository<Vehicle> vehicleRepository)
    {
        _parkingEventRepository = parkingEventRepository;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<List<Vehicle>> GetVehiclesInGarageAsync()
    {
        var parkingEvents = await _parkingEventRepository.GetAll();
        var vehiclesInGarage = parkingEvents.Where(pe => pe.CheckOutTime == null).Select(pe => pe.Vehicle);
        return vehiclesInGarage.ToList();
    }

    public async Task<bool> IsVehicleParkedAsync(int VehicleId)
    {
        var vehiclesInGarage = await GetVehiclesInGarageAsync();
        var isVehicleParked = vehiclesInGarage.Any(v => v.VehicleId == VehicleId);
        return isVehicleParked;
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

    public async Task<ParkingEvent> ParkVehicleAsync(int VehicleId)
    {
        var vehicle = await _vehicleRepository.Get(VehicleId);
        var parkingEvent = new ParkingEvent
        {
            Vehicle = vehicle,
            CheckInTime = DateTime.Now,
        };
        await _parkingEventRepository.Add(parkingEvent);
        return parkingEvent;
    }

    public async Task<ParkingEvent> UnParkVehicleAsync(int VehicleId)
    {
        var vehicle = await _vehicleRepository.Get(VehicleId);
        var parkingEvents = await _parkingEventRepository.GetAll();
        var parkingEvent = parkingEvents.Where(pe => pe.Vehicle == vehicle && pe.CheckOutTime == null).FirstOrDefault();
        parkingEvent.CheckOutTime = DateTime.Now;
        await _parkingEventRepository.Update(parkingEvent);
        return parkingEvent;
    }
}
