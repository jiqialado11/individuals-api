using Individuals.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Individuals.Persistance.TypeConfigurations
{
    public class PhoneNumberTypeConfiguration:IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.Property(x => x.Number).IsRequired();
        }
    }
}
