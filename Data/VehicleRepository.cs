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
            return await _context.Vehicles.Include(v => v.Owner).Include(v => v.VehicleType).FirstOrDefaultAsync(v => v.VehicleId == id);
        }

        public Task<Vehicle> SearchByString(string value)
        {
            return _context.Vehicles.FirstOrDefaultAsync(v => v.RegNumber == value);
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
    }
}
