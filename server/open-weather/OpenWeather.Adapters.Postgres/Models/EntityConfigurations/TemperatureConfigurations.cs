using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OpenWeather.Adapters.Postgres.Models.EntityConfigurations;

public class TemperatureConfiguration : IEntityTypeConfiguration<Temperature>
{
    public void Configure(EntityTypeBuilder<Temperature> builder)
    {
        builder
            .Property(b => b.TemperatureC)
            .IsRequired();
        
        builder
            .Property(b => b.Latitude)
            .IsRequired();
        
        builder
            .Property(b => b.Longitude)
            .IsRequired();
        
        builder
            .Property(b => b.TimeStamp)
            .IsRequired();
        
        
        builder.HasIndex(i => new { i.Latitude, i.Longitude }); //for efficient location-based queries
        builder.HasIndex(i => new { i.TimeStamp ,i.Latitude, i.Longitude }); //for efficient time-based queries (start-end)
        
        //TODO incorporate spatial or geospatial indexing for "find everything within a certain radius or bounding box."
    }
}