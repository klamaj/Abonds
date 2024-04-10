using Core.Models.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ClientInterestsConfiguration : IEntityTypeConfiguration<ClientInerestModel>
    {
        public void Configure(EntityTypeBuilder<ClientInerestModel> builder)
        {
            builder.HasKey(ci => new { ci.ClientId, ci.SubInterestId });
            // many-to-many
            builder.HasOne(ci => ci.Client)
                .WithMany(c => c.ClientInterests)
                .HasForeignKey(ci => ci.ClientId);
            builder.HasOne(si => si.SubInterest)
                .WithMany(s => s.ClientInterests)
                .HasForeignKey(si => si.ClientId);
        }
    }
}