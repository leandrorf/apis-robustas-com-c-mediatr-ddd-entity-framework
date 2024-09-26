using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Infra.Repositories.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoEntity>
    {
        public void Configure( EntityTypeBuilder<ContatoEntity> builder )
        {
            builder.ToTable( "Contato" );

            ////Propriedades
            builder.HasKey( x => x.Id );
            builder.Property( x => x.Nome ).HasMaxLength( 150 ).IsRequired( );
            builder.Property( x => x.Telefone ).HasMaxLength( 150 ).IsRequired( );
            builder.Property( x => x.Nicho ).IsRequired( );

            //Foreikey
            builder.HasOne( x => x.Usuario ).WithMany( ).HasForeignKey( "IdUsuario" );
        }
    }
}