using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VemDeZap.Domain.Entities;

namespace VemDeZap.Infra.Repositories.Map
{
    public class EnvioMap : IEntityTypeConfiguration<EnvioEntity>
    {
        public void Configure( EntityTypeBuilder<EnvioEntity> builder )
        {
            builder.ToTable( "Envio" );

            ////Propriedades
            builder.HasKey( x => x.Id );
            //builder.Property(x => x.Nome).HasMaxLength(150).IsRequired();
            builder.Property( x => x.Enviado ).IsRequired( );

            //Foreikey
            builder.HasOne( x => x.Campanha ).WithMany( ).HasForeignKey( "IdCampanha" );
            builder.HasOne( x => x.Grupo ).WithMany( ).HasForeignKey( "IdGrupo" );
            builder.HasOne( x => x.Contato ).WithMany( ).HasForeignKey( "IdContato" );
        }
    }
}