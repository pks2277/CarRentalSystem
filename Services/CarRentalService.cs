using CarRentalSystem.Models;
using CarRentalSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalSystem.Services
{
    public class CarRentalService : ICarRentalService
    {
        private readonly ICarRepository _carRepository;
        private readonly EmailService _emailService;


        public CarRentalService(ICarRepository carRepository, EmailService emailService)
        {
            _carRepository = carRepository;
            _emailService = emailService;
        }

        public async Task<Car> RentCar(int carId, RentCarRequest request)
        {
            var car = await _carRepository.GetCarById(carId);
            if (car != null && car.IsAvailable)
            {
                car.IsAvailable = false;  
                await _carRepository.UpdateCarAvailability(carId, false);

                
                await _emailService.SendCarRentalConfirmationEmail(request.Email, request.Name, car.Make, car.Model);
                return car;
            }

            return null;  
        }

        public async Task<IEnumerable<Car>> GetAvailableCars()
        {
            return await _carRepository.GetAvailableCars();
        }

        public async Task AddCar(Car car)
        {
            await _carRepository.AddCar(car);
        }

        
        public async Task DeleteCar(int carId)
        {
            await _carRepository.DeleteCar(carId);
        }

        public async Task<Car> UpdateCar(int id, Car car)
        {
            var existingCar = await _carRepository.GetCarById(id);
            if (existingCar == null)
            {
                return null;  
            }

            
            existingCar.Make = car.Make ?? existingCar.Make;
            existingCar.Model = car.Model ?? existingCar.Model;
            existingCar.Year = car.Year != 0 ? car.Year : existingCar.Year;
            existingCar.PricePerDay = car.PricePerDay != 0 ? car.PricePerDay : existingCar.PricePerDay;
            existingCar.IsAvailable = car.IsAvailable != existingCar.IsAvailable ? car.IsAvailable : existingCar.IsAvailable;

            
            await _carRepository.UpdateCar(existingCar);
            return existingCar;
        }

    }
}
