using Core.Models.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ClientConfiguration : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> builder)
        {
            builder.HasOne<ClientModel>(c => c.MatchedUser)
                .WithOne()
                .HasForeignKey<ClientModel>(u => u.MatchedUserId)
                .IsRequired(false);
            
            builder.HasOne(c => c.Contract)
                .WithOne(x => x.Client)
                .HasForeignKey<ContractModel>(x => x.ClientId);
        }
    }
}