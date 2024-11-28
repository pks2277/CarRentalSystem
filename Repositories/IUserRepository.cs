using System.Threading.Tasks;
using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}
