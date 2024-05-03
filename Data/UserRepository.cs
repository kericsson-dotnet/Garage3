using Microsoft.EntityFrameworkCore;
using Garage.Models;

namespace Garage.Data;

public class UserRepository : IRepository<User>
{
    private readonly GarageDbContext _context;

    public UserRepository(GarageDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> Get(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}