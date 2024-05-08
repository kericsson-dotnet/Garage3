using Garage.Models;

namespace Garage.ViewModels;

public class GarageViewModel
{
    public int MaxSlots { get; set; }
    public List<Vehicle> VehiclesInGarage { get; set; }

    public GarageViewModel()
    {
        MaxSlots = 50;
    }
}
