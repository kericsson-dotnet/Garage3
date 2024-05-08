using Garage.Models;
using Garage.ViewModels;

namespace Garage.Services;

public interface IGarageService
{
    Task<GarageViewModel> GetGarageStatusAsync();
    Task<List<Vehicle>> GetVehiclesInGarageAsync();
    Task<bool> IsVehicleParkedAsync(int vehicleId);
    Task<ParkingEvent> ParkVehicleAsync(int vehicleId);
    Task<ParkingEvent> UnParkVehicleAsync(int vehicleId);
}
