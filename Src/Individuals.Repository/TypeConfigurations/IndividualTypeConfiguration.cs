using Individuals.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Individuals.Persistance.TypeConfigurations
{
    public class IndividualTypeConfiguration:IEntityTypeConfiguration<Individual>
    {
        public void Configure(EntityTypeBuilder<Individual> builder)
        {
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PersonalNumber).HasMaxLength(11).IsRequired();

            builder.HasOne(x => x.City).WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.PhoneNumbers).WithOne().IsRequired().OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
