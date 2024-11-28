using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDBContext _context;

        public CarRepository(CarRentalDBContext context)
        {
            _context = context;
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<IEnumerable<Car>> GetAvailableCars()
        {
            return await _context.Cars.Where(c => c.IsAvailable).ToListAsync();
        }

        public async Task AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAvailability(int id, bool isAvailable)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                car.IsAvailable = isAvailable;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateCar(Car car)
        {
            _context.Cars.Update(car);  
            await _context.SaveChangesAsync();  
        }

    }
}
