using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class SensorDataContext : DbContext
    {
        public SensorDataContext(DbContextOptions<SensorDataContext> options)
            : base(options)
        {
        }

        public DbSet<SensorData> SensorData { get; set; }

    }
}