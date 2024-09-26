using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Entities.Base;
using VemDeZap.Domain.Enums;

namespace VemDeZap.Domain.Entities
{
    public class GrupoEntity : EntityBase
    {
        public string Nome { get; set; }
        public EnumNicho Nicho { get; set; }
        public UsuarioEntity Usuario { get; set; }

        public GrupoEntity( UsuarioEntity usuario, string nome, EnumNicho nicho )
        {
            Usuario = usuario;
            Nome = nome;
            Nicho = nicho;

            if ( usuario == null )
            {
                AddNotification( "Usuario", "Informe o usuário" );
            }

            new AddNotifications<GrupoEntity>( this )
                .IfNullOrInvalidLength( x => x.Nome, 3, 150 )
                .IfEnumInvalid( x => x.Nicho );
        }

        public GrupoEntity( )
        {
        }

        public void AlterarGrupo( string nome, EnumNicho nicho )
        {
            Nome = nome;
            Nicho = nicho;
        }
    }
}