using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Entities;
using VemDeZap.Infra.Repositories.Map;

namespace VemDeZap.Infra.Repositories.Base
{
    public class DataContext : DbContext
    {
        public DbSet<UsuarioEntity> Usuario { get; set; }
        public DbSet<ContatoEntity> Contato { get; set; }
        public DbSet<EnvioEntity> Envio { get; set; }
        public DbSet<GrupoEntity> Grupo { get; set; }
        public DbSet<CampanhaEntity> Campanha { get; set; }

        public DataContext( DbContextOptions<DataContext> options ) : base( options )
        {
            Database.Migrate( );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Ignore<Notification>( );

            modelBuilder.Entity<UsuarioEntity>( new UsuarioMap( ).Configure );
            modelBuilder.Entity<GrupoEntity>( new GrupoMap( ).Configure );
            modelBuilder.Entity<ContatoEntity>( new ContatoMap( ).Configure );
            modelBuilder.Entity<CampanhaEntity>( new CampanhaMap( ).Configure );
            modelBuilder.Entity<EnvioEntity>( new EnvioMap( ).Configure );
        }
    }
}