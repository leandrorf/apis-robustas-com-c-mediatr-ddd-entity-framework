using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Infra.Repositories.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioEntity>
    {
        public void Configure( EntityTypeBuilder<UsuarioEntity> builder )
        {
            builder.ToTable( "Usuario" );

            ////Propriedades
            builder.HasKey( x => x.Id );
            builder.Property( x => x.PrimeiroNome ).HasMaxLength( 150 ).IsRequired( );
            builder.Property( x => x.UltimoNome ).HasMaxLength( 150 ).IsRequired( );
            builder.Property( x => x.Email ).HasMaxLength( 200 ).IsRequired( );
            builder.Property( x => x.Senha ).HasMaxLength( 36 ).IsRequired( );
            builder.Property( x => x.DataCadastro ).IsRequired( );
            builder.Property( x => x.Ativo ).IsRequired( );
        }
    }
}