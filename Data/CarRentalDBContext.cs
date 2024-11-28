using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Models;

namespace CarRentalSystem.Data
{
    public class CarRentalDBContext : DbContext
    {
        public CarRentalDBContext(DbContextOptions<CarRentalDBContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
