using Microsoft.EntityFrameworkCore;
using Garage.Models;

namespace Garage.Data;

public class ParkingEventRepository : IRepository<ParkingEvent>
{
    private readonly GarageDbContext _context;

    public ParkingEventRepository(GarageDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ParkingEvent>> GetAll()
    {
        return await _context.ParkingEvents.Include(pe => pe.Vehicle).ToListAsync();
    }

    public async Task<ParkingEvent> Get(int id)
    {
        return await _context.ParkingEvents.Include(pe => pe.Vehicle).AsNoTracking().FirstAsync(pe => pe.ParkingEventId == id);
    }

    public async Task Add(ParkingEvent parkingEvent)
    {
        _context.ParkingEvents.Add(parkingEvent);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ParkingEvent parkingEvent)
    {
        _context.Entry(parkingEvent).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(ParkingEvent parkingEvent)
    {
        _context.ParkingEvents.Remove(parkingEvent);
        await _context.SaveChangesAsync();
    }
}