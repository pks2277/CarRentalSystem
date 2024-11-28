using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetCarById(int id);
        Task<IEnumerable<Car>> GetAvailableCars();
        Task AddCar(Car car);
        Task UpdateCarAvailability(int id, bool isAvailable);
        Task DeleteCar(int id);
        Task UpdateCar(Car car);
    }
}
