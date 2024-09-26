using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Infra.Repositories.Map
{
    public class GrupoMap : IEntityTypeConfiguration<GrupoEntity>
    {
        public void Configure( EntityTypeBuilder<GrupoEntity> builder )
        {
            builder.ToTable( "Grupo" );

            ////Propriedades
            builder.HasKey( x => x.Id );
            builder.Property( x => x.Nome ).HasMaxLength( 150 ).IsRequired( );
            builder.Property( x => x.Nicho ).IsRequired( );

            //Foreikey
            builder.HasOne( x => x.Usuario ).WithMany( ).HasForeignKey( "IdUsuario" );
        }
    }
}