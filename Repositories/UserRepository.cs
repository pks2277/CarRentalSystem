using System.Threading.Tasks;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDBContext _context;

        public UserRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
