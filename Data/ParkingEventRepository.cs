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
        return await _context.ParkingEvents.ToListAsync();
    }

    public async Task<ParkingEvent> Get(int id)
    {
        return await _context.ParkingEvents.FindAsync(id);
    }

    public async Task Add(ParkingEvent ParkingEvent)
    {
        _context.ParkingEvents.Add(ParkingEvent);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ParkingEvent ParkingEvent)
    {
        _context.Entry(ParkingEvent).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var ParkingEvent = await _context.ParkingEvents.FindAsync(id);
        _context.ParkingEvents.Remove(ParkingEvent);
        await _context.SaveChangesAsync();
    }
}