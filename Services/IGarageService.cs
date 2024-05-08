using Garage.Models;
using Garage.ViewModels;

namespace Garage.Services;

public interface IGarageService
{
    Task<GarageViewModel> GetGarageStatusAsync();
    Task<List<Vehicle>> GetVehiclesInGarageAsync();
}
