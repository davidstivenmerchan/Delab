using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delab.AccessData.Data.ModelConfig;

public class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(e => e.CityId);
        builder.HasIndex(e => new { e.Name, e.StateId }).IsUnique();
        builder.HasOne(e => e.State).WithMany(c => c.Cities).OnDelete(DeleteBehavior.Restrict);
    }
}
