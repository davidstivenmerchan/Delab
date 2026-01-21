using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delab.AccessData.Data.ModelConfig;

public class StateConfig : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.StateId);
        builder.HasIndex(e => new { e.Name, e.CountryId }).IsUnique();
        builder.HasOne(e => e.Country).WithMany(e => e.States).OnDelete(DeleteBehavior.Restrict);
    }
}
