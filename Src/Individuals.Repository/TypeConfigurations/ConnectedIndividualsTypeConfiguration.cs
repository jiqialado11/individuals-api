using Individuals.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Individuals.Persistance.TypeConfigurations
{
    public class ConnectedIndividualsTypeConfiguration:IEntityTypeConfiguration<ConnectedIndividual>
    {
        public void Configure(EntityTypeBuilder<ConnectedIndividual> builder)
        {
            builder.Property<long>("ConnectedFromIndividualId");
            builder.Property<long>("ConnectedToIndividualId");
            builder.HasKey("ConnectedFromIndividualId", "ConnectedToIndividualId");
            builder.HasOne(x => x.ConnectedFromIndividual).WithMany(x => x.ConnectedIndividuals).HasForeignKey("ConnectedFromIndividualId").IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ConnectedToIndividual).WithMany().HasForeignKey("ConnectedToIndividualId").IsRequired().OnDelete(DeleteBehavior.Restrict);
          
        }
    }
}
