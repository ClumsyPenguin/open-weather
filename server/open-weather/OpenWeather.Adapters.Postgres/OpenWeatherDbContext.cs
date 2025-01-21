using Microsoft.EntityFrameworkCore;
using OpenWeather.Adapters.Postgres.Models;

namespace OpenWeather.Adapters.Postgres;

public class OpenWeatherDbContext : DbContext
{
    public OpenWeatherDbContext(DbContextOptions<OpenWeatherDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Temperature> Temperatures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        

        DbInitializer.Initialize(this);
    }
}