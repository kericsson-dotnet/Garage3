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

    public async Task Add(VehicleType vehicletype)
    {
        _context.VehicleTypes.Add(vehicletype);
        await _context.SaveChangesAsync();
    }

    public async Task Update(VehicleType vehicletype)
    {
        _context.Entry(vehicletype).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var vehicletype = await _context.VehicleTypes.FindAsync(id);
        _context.VehicleTypes.Remove(vehicletype);
        await _context.SaveChangesAsync();
    }
}
