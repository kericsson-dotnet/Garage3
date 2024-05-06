using Garage.Models;
using Microsoft.EntityFrameworkCore;

namespace Garage.Data
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly GarageDbContext _context;

        public VehicleRepository(GarageDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> Get(int id)
        {
            return await _context.Vehicles.Include(v => v.Owner).Include(v => v.VehicleType).AsNoTracking().FirstAsync(v => v.VehicleId == id);
        }
        
        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await _context.Vehicles.Include(v => v.Owner).Include(v => v.VehicleType).ToListAsync();
        }

        public async Task Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Vehicle vehicle)
        {
           _context.Entry(vehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Vehicle vehicle)
        {
            
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
        }



    }
}
