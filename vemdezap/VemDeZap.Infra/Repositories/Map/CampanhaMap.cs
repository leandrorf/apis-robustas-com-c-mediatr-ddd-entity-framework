using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Infra.Repositories.Map
{
    public class CampanhaMap : IEntityTypeConfiguration<CampanhaEntity>
    {
        public void Configure( EntityTypeBuilder<CampanhaEntity> builder )
        {
            builder.ToTable( "Campanha" );

            ////Propriedades
            builder.HasKey( x => x.Id );
            builder.Property( x => x.Nome ).HasMaxLength( 150 ).IsRequired( );

            //Foreikey
            builder.HasOne( x => x.Usuario ).WithMany( ).HasForeignKey( "IdUsuario" );
        }
    }
}