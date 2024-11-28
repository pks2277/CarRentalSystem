using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    public interface ICarRentalService
    {
        Task<Car> RentCar(int carId, RentCarRequest request);
        Task<IEnumerable<Car>> GetAvailableCars();
        Task AddCar(Car car);
        Task DeleteCar(int carId);  
        Task<Car> UpdateCar(int carId, Car car);  
    }
}
