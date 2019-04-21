using Individuals.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Individuals.Persistance.TypeConfigurations
{
    public class CityTypeConfiguration:IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasData(new
            {
                Id = (long)1,
                Name = "Tbilisi"
            }, new
            {
                Id = (long)2,
                Name = "Batumi"
            }, new
            {
                Id = (long)3,
                Name = "Kutaisi"
            }, new
            {
                Id = (long)4,
                Name = "Telavi"
            });
        }
    }
}
