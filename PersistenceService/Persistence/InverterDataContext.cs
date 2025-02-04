using Microsoft.EntityFrameworkCore;
using PersistenceService.Models;


namespace PersistenceService.Persistence
{
    public class InverterDataContext : DbContext
    {
        public InverterDataContext(DbContextOptions<InverterDataContext> options) 
            : base(options) 
        {
        }

        public DbSet<InverterData> InverterDatas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapping della tabella
            modelBuilder.Entity<InverterData>()
                .ToTable("InverterData"); 
            
            // In caso servano constraint, chiavi uniche, ecc. si aggiungono qui
        }
    }
}