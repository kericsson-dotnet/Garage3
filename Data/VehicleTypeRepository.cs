using Garage.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage.Data;
public class VehicleTypeRepository : IRepository<VehicleType>
{
    private readonly GarageDbContext _context;

    public VehicleTypeRepository(GarageDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VehicleType>> GetAll()
    {
        return await _context.VehicleTypes.ToListAsync();
    }

    public async Task<VehicleType> Get(int id)
    {
        return await _context.VehicleTypes.FindAsync(id);
    }

    public Task<VehicleType> SearchByString(string value)
    {
        throw new NotImplementedException();
    }

    public async Task Add(VehicleType vehicleType)
    {
        _context.VehicleTypes.Add(vehicleType);
        await _context.SaveChangesAsync();
    }

    public async Task Update(VehicleType vehicleType)
    {
        _context.Entry(vehicleType).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(VehicleType vehicleType)
    {
        _context.VehicleTypes.Remove(vehicleType);
        await _context.SaveChangesAsync();
    }

}
