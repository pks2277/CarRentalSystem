using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;

        public CarController(ICarRentalService carRentalService)
        {
            _carRentalService = carRentalService;
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars()
        {
            var cars = await _carRentalService.GetAvailableCars();
            return Ok(cars);
        }

        
        [HttpPost("rent")]
        [Authorize]
        public async Task<IActionResult> RentCar(int carId, [FromBody] RentCarRequest request)
        {
            var car = await _carRentalService.RentCar(carId, request);
            if (car == null)
            {
                return NotFound("Car not available.");
            }
            return Ok(car);
        }
    

        
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            await _carRentalService.AddCar(car);
            return Ok("Car added successfully.");
        }

     
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carRentalService.DeleteCar(id);
            return Ok("Car deleted successfully.");
        }

        
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            var updatedCar = await _carRentalService.UpdateCar(id, car);
            if (updatedCar == null)
            {
                return NotFound("Car not found.");
            }
            return Ok("Car updated successfully.");
        }
    }
}
