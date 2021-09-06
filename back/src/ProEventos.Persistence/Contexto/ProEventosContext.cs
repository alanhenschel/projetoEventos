
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexto
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // [N N]
            modelBuilder.Entity<PalestranteEvento>()
                        .HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            // When table has two keys, define Delete cascade
            modelBuilder.Entity<Evento>()
                        .HasMany(e => e.RedesSociais)
                        .WithOne(rs => rs.Evento)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                        .HasMany(e => e.RedesSociais)
                        .WithOne(rs => rs.Palestrante)
                        .OnDelete(DeleteBehavior.Cascade);
        }

        // This inner class it's nescessary for the migrations
        public class ProEventosContextDesignFactory : IDesignTimeDbContextFactory<ProEventosContext>
        {
            public ProEventosContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ProEventosContext>()
                                        .UseNpgsql("Host=localhost;Port=5432;Database=pro_eventos;Username=app_user;Password=app_user");
                return new ProEventosContext(optionsBuilder.Options);
            }
        }
    }
}