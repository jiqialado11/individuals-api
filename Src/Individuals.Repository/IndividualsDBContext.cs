using Individuals.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Individuals.Persistance
{
    public class IndividualsDBContext:DbContext
    {
        public IndividualsDBContext(DbContextOptions<IndividualsDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ConnectedIndividual> ConnectedIndividuals { get; set; }
    }
}
