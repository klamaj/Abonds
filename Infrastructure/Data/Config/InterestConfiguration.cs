using Core.Models.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class InterestConfiguration : IEntityTypeConfiguration<InterestModel>
    {
        public void Configure(EntityTypeBuilder<InterestModel> builder)
        {
            builder.HasMany(i => i.SubInterests)
                .WithOne(s => s.Interest)
                .HasForeignKey(s => s.IntrestId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.InterestColor).HasMaxLength(7);
        }
    }
}